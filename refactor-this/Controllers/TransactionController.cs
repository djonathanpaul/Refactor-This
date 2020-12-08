using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace refactor_this.Controllers
{
    [RoutePrefix("api/Accounts/{id:guid}/Transactions")]
    public class TransactionController : ApiController
    {
        
        [HttpGet, Route("")]
        public IHttpActionResult GetTransactions(Guid id)
        {
            try
            {
                using (RefractorContext entities = new RefractorContext())
                {
                    var transactions = entities.Transactions.Where(transaction => transaction.AccountId == id).Select(transaction => new { transaction.Amount, transaction.Date }).ToList();
                    Console.WriteLine(transactions);
                    if (transactions.Count == 0)
                    {
                        return Content(HttpStatusCode.NotFound, "Account ID not found");

                    }
                    else
                    {
                        return Content(HttpStatusCode.OK, transactions);
                    }

                }
            }catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

       

        [HttpPost, Route("")]
        public IHttpActionResult AddTransaction(Guid id, [FromBody]Transaction transaction)
        {
            
            using (RefractorContext entities = new RefractorContext())
            {
                using (DbContextTransaction dbtransaction = entities.Database.BeginTransaction()) {

                    try
                    {
                        
                        transaction.Id = Guid.NewGuid();
                        transaction.AccountId = id;
                        transaction.Date = DateTime.Now;
                        entities.Transactions.Add(transaction);

                        var clientAccount = entities.Accounts.FirstOrDefault(account => account.Id == id);
                        if (clientAccount == null)
                        {
                            return Content(HttpStatusCode.NotFound, "Account ID not found");
                        }

                        clientAccount.Amount = clientAccount.Amount + transaction.Amount;

                        entities.SaveChanges();

                        dbtransaction.Commit();
                        return Content(HttpStatusCode.OK, clientAccount);
                    }
                    catch (Exception ex)
                    {
                        dbtransaction.Rollback();
                        return Content(HttpStatusCode.BadRequest, ex.Message);
                        
                    }
                }
                   
            }

           

        }
    }
}