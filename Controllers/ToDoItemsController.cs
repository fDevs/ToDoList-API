using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using api.fdevs.com.Entities;
using Newtonsoft.Json;

namespace api.fdevs.com.Controllers
{
    
    public class ToDoItemsController : Controller
    {
        private fdevscom_apiEntities db = new fdevscom_apiEntities();
        private const int NO_USER_FOUND = -1;


        private int ReturnUserIdFromAPIKey(Guid APIKey)
        {
            if(db.APIKeys.Any(x=>x.APIKey == APIKey))
            {
                return db.APIKeys.Where(x => x.APIKey == APIKey).Select(x => x.UserId).First();
            }
            else
            {
                return NO_USER_FOUND;
            }

        }

        // GET: ToDoItems
        [AllowCrossSite]
        public ActionResult Index(Guid APIKey)
        {
            int userid = ReturnUserIdFromAPIKey(APIKey);
            if (db.ToDoItems.Where(x => x.UserId == userid).Any())
            {
                var items = db.ToDoItems.Where(x => x.UserId == userid && x.IsArchived != true)
                                        .Join(db.ToDoItemDetails, item => item.ToDoItemId, details=> details.ToDoItemId, (item, details) => new { ToDoItems = item, ToDoItemDetails = details})
                                                .ToList();

                return Json(items, 
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
        }

        // GET: ToDoItems/Details/5
        [AllowCrossSite]
        public async Task<ActionResult> Details(Guid APIKey, int? id)
        {
            int userid = ReturnUserIdFromAPIKey(APIKey);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(db.ToDoItems.Any(x => x.UserId == userid && x.ToDoItemId == id))
            {
                ToDoItems toDoItems = await db.ToDoItems.FindAsync(id);
                return Json(toDoItems);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }  
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowCrossSite]
        public int Create(Guid APIKey, string ToDoItem)
        {
            if (ModelState.IsValid)
            {
                ToDoItems toDoItem = new ToDoItems();
                toDoItem.ToDoItem = ToDoItem;
                toDoItem.UserId = ReturnUserIdFromAPIKey(APIKey);
                db.ToDoItems.Add(toDoItem);
                db.SaveChanges();

                ToDoItemDetails toDoItemDetails = new ToDoItemDetails();
                toDoItemDetails.DateCreated = DateTime.Now;
                toDoItemDetails.ToDoItemId = toDoItem.ToDoItemId;
                db.ToDoItemDetails.Add(toDoItemDetails);
                db.SaveChanges();


                return toDoItem.ToDoItemId;
            }
            else
            {
                return -1;
            }
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowCrossSite]
        public ActionResult Edit(Guid APIKey, int id, string toDoItem, bool? isCompleted, bool? isArchived, DateTime? dateDue, string description)
        {
            int userid = ReturnUserIdFromAPIKey(APIKey);

            if (db.ToDoItems.Any(x => x.UserId == userid && x.ToDoItemId == id))
            {
                ToDoItems singleToDoItem = db.ToDoItems.Where(x=>x.ToDoItemId == id).First();
                ToDoItemDetails singleToDoItemDetails = db.ToDoItemDetails.Where(x => x.ToDoItemId == id).First();
                if (toDoItem != null)
                {
                    singleToDoItem.ToDoItem = toDoItem;
                }
                if(isCompleted.HasValue)
                {
                    singleToDoItem.IsCompleted = isCompleted.Value;
                }
                if (isArchived.HasValue)
                {
                    singleToDoItem.IsArchived = isArchived.Value;
                }
                if (description != null)
                {
                    singleToDoItemDetails.Description = description;
                }
                if (dateDue.HasValue)
                {
                    singleToDoItemDetails.DateDue = dateDue;
                }
                try
                {
                    db.Entry(singleToDoItem).State = EntityState.Modified;
                    db.Entry(singleToDoItemDetails).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }

    }
}


