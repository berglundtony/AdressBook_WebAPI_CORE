using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdressBook_WebAPI_CORE.Data;
using static AdressBook_WebAPI_CORE.Controller.ValuesController;
using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace AdressBook_WebAPI_CORE.Controller
{
    [Produces("application/json")]
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {


        List<IEnumerable> _listresults = new List<IEnumerable>();
        /// <summary>
        /// Get a list of all contacts
        /// </summary>
        /// <returns></returns>

        // GET: api/Contact
        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return ContactDataStore.Current._Contacts;
        }

        /// <summary>
        /// This will provide all values for a specific Id that is being passed.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>

        // GET: api/Contact/5
        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult GetContact(int id, string fields = "all")
        {
            IEnumerable shapedResults;
            IEnumerable Results;
            bool currentpropertybool = true;
            try
            {
                var contact = ContactDataStore.Current._Contacts.Where(i => i.Id == id);

                if (contact == null)
                {
                    return NotFound();
                }
                if (!string.Equals(fields, "all", StringComparison.OrdinalIgnoreCase))
                {

                    Results = ContactDataStore.Current._Contacts.Where(i => i.Id == id).Select(x => x);


                    var serializedContact = JsonConvert.SerializeObject(Results, Formatting.Indented);

                    dynamic data = JValue.Parse(serializedContact.ToString());

                    foreach (dynamic item in data)
                    {
                        JArray result = new JArray(item);


                        foreach (JObject content in result.Children<JObject>())
                        {
                            foreach (JProperty parsedProperty in content.Properties())
                            {
                                string propertyName = parsedProperty.Name;

                                if (propertyName.Equals("AdressInfo"))
                                {
                                    foreach (JProperty adressProperty in content["AdressInfo"])
                                    {
                                        string name = adressProperty.Name;
                                        JToken value = adressProperty.Value;

                                        propertyName = adressProperty.Name.ToLower();
                                        if (propertyName.Equals(fields.ToLower()))
                                        {
                                            shapedResults = ContactDataStore.Current._Contacts.Where(i => i.Id == id).Select(x => GetShapedObject(x.AdressInfo, fields));
                                            return Ok(shapedResults);
                                        }
                                    }
                                }
                                else
                                {
                                    propertyName = parsedProperty.Name.ToLower();

                                    if (propertyName.Equals(fields.ToLower()))
                                    {
                                        var value = parsedProperty.Value;
                                        shapedResults = ContactDataStore.Current._Contacts.Where(i => i.Id == id).Select(x => GetShapedObject(x, fields));
                                        return Ok(shapedResults);
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    return Ok(contact);
                }
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

        }

        private IActionResult InternalServerError()
        {
            throw new NotImplementedException();
        }

        private object GetShapedObject<TParameter>(TParameter entity, string fields)
        {
            if (string.IsNullOrEmpty(fields))
                return entity;
            Regex regex = new Regex(@"[^,()]+(\([^()]*\))?");

            var requestedFields = regex.Matches(fields).Cast<Match>().Select(m => m.Value).Distinct();
            ExpandoObject expando = new ExpandoObject();
            foreach (var field in requestedFields)
            {
                if (field.Contains("("))
                {
                    var navField = field.Substring(0, field.IndexOf('('));


                    IList navFieldValue = entity.GetType()
                                                ?.GetProperty(navField, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
                                                ?.GetValue(entity, null) as IList;
                    var regexMatch = Regex.Matches(field, @"\((.+?)\)");
                    if (regexMatch?.Count > 0)
                    {
                        var propertiesString = regexMatch[0].Value?.Replace("(", string.Empty).Replace(")", string.Empty);
                        if (!string.IsNullOrEmpty(propertiesString))
                        {
                            string[] navigationObjectProperties = propertiesString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            List<object> list = new List<object>();
                            foreach (var item in navFieldValue)
                            {
                                list.Add(GetShapedObject(item, navigationObjectProperties));
                            }

                            ((IDictionary<string, object>)expando).Add(navField, list);
                        }
                    }
                }
                else
                {
                    var value = entity.GetType()
                                      ?.GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
                                      ?.GetValue(entity, null);

                    ((IDictionary<string, object>)expando).Add(field, value);
                }
            }

            return expando;
        }

        private object GetShapedObject<TParameter>(TParameter entity, IEnumerable<string> fields)
        {
            ExpandoObject expando = new ExpandoObject();
            foreach (var field in fields)
            {
                var value = entity.GetType()
                                  ?.GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                                  ?.GetValue(entity, null);
                ((IDictionary<string, object>)expando).Add(field, value);
            }
            return expando;

        }

        /// <summary>
        /// This will provide all values for a specific Id and an input query that is being passed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        // POST: api/Contact/id
        [HttpPost("{Id}")]
        public IActionResult CreateNewContact(int id, [FromBody] ContactForCreation newContact)
        {
            var contact = ContactDataStore.Current._Contacts.FirstOrDefault(i => i.Id == id);

            if (newContact == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxContactId = ContactDataStore.Current._Contacts.Select(c => c).Max(p => p.Id);

            var finalContact = new Contact()
            {
                Id = ++maxContactId,
                Firstname = newContact.Firstname,
                Lastname = newContact.Lastname,
                DateOfBirth = newContact.DateOfBirth,
                Mobile = newContact.Mobile,
                AdressInfo = new Contact.Adress
                {
                    Id = ++maxContactId,
                    Street = newContact.AdressInfo.Street,
                    PostalCode = newContact.AdressInfo.PostalCode,
                    City = newContact.AdressInfo.City
                }
            };
            ContactDataStore.Current._Contacts.Add(finalContact);
            return CreatedAtRoute("GetContact", new { Id = finalContact.Id });
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] ContactForUpdate updateContact)
        {
            var contact = ContactDataStore.Current._Contacts.FirstOrDefault(x => x.Id == id);

            if(updateContact == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (contact == null)
            {
                return NotFound();
            }

            contact.Firstname = updateContact.Firstname;
            contact.Lastname = updateContact.Lastname;
            contact.Mobile = updateContact.Mobile;
            contact.DateOfBirth = updateContact.DateOfBirth;
            contact.AdressInfo.Street = updateContact.AdressInfo.Street;
            contact.AdressInfo.PostalCode = updateContact.AdressInfo.PostalCode;
            contact.AdressInfo.City = updateContact.AdressInfo.City;

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}