using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace refactor_this.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountController : ApiController
    {
        

        [HttpGet, Route("{id:guid}")]
        public IHttpActionResult GetById(Guid id)
        {
            try
            {
                using (RefractorContext entities = new RefractorContext())
                {
                    Account account = entities.Accounts.FirstOrDefault(acc => acc.Id == id);

                    if (account == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Account ID not found");

                    }
                    else
                    {
                        return Content(HttpStatusCode.OK,account);
                    }

                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }

        }


        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                using (RefractorContext entities = new RefractorContext())
                {
                    var accounts = entities.Accounts.ToList();

                    if (accounts.Count == 0)
                    {
                        return Content(HttpStatusCode.OK, "There are no Accounts");

                    }
                    else
                    {
                        return Content(HttpStatusCode.OK, accounts);
                    }

                }

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }


        }


        [HttpPost, Route("")]
        public IHttpActionResult Add([FromBody]Account account)
        {
            try
            {
                using (RefractorContext entities = new RefractorContext())
                {
                    
                    {
                        account.Id = Guid.NewGuid();
                        entities.Accounts.Add(account);
                        entities.SaveChanges();
                        return Content(HttpStatusCode.Created, account);

                    }

                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut, Route("{id:guid}")]
        public IHttpActionResult Update(Guid id, [FromBody] Account account)
        {
            try
            {
                using (RefractorContext entities = new RefractorContext())
                {
                    var existingAccount = entities.Accounts.FirstOrDefault(acc => acc.Id == id);

                    if (existingAccount == null)
                    {

                        return Content(HttpStatusCode.NotFound, "Account with ID: " + id.ToString() + " could not be found to update");
                    }
                    else
                    {
                        existingAccount.Name = account.Name;
                        existingAccount.Amount = account.Amount;
                        existingAccount.Number = account.Number;
                        entities.SaveChanges();
                        return Content(HttpStatusCode.OK, existingAccount);

                    }

                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        
        [HttpDelete, Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                using (RefractorContext entities = new RefractorContext())
                {
                    var existingAccount = entities.Accounts.FirstOrDefault(acc => acc.Id == id);

                    if (existingAccount == null)
                    {

                        return Content(HttpStatusCode.NotFound, "Account with ID: " + id.ToString() + " could not be found to delete");
                    }
                    else
                    {
                        entities.Accounts.Remove(existingAccount);
                        entities.SaveChanges();
                        return Content(HttpStatusCode.OK,"Account with ID: "+ id.ToString()+" has been deleted");
                    }
                }
            }

            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}