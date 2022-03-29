using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core;
using Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FriendsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : Controller
    {
        private readonly friendDbContext _context;


        public FriendsController(friendDbContext context)
        {
            _context = context;
        }

        // GET: Friends
        public async Task<IActionResult> Index()
        {
            return View(await _context.Friend.ToListAsync());
        }

        #region GETFriends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> Getfriends(string search)
        {
            ViewData["SearchFilter"] = search;

            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            string consult = "ConsultFriends";
            SqlCommand cmd = new SqlCommand(consult, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", search);

            return View(await _context.Friend.ToListAsync());
        }

        #endregion

        #region Create
       
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Friend friend)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "PostFriend";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", friend.Name);
                cmd.Parameters.AddWithValue("@Lastname", friend.Lastname);
                cmd.Parameters.AddWithValue("@Telphone", friend.Telphone);
                cmd.Parameters.AddWithValue("@Email", friend.Email);
                cmd.Parameters.AddWithValue("@Birthday", friend.Birthday);
              
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return View(await _context.Friend.ToListAsync());
        }

        #endregion


        #region Details
        // GET: Friends/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        #endregion


        #region Edit
        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Friend friend)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "EditFriend";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", friend.Name);
                cmd.Parameters.AddWithValue("@Lastname", friend.Lastname);
                cmd.Parameters.AddWithValue("@Telphone", friend.Telphone);
                cmd.Parameters.AddWithValue("@Email", friend.Email);
                cmd.Parameters.AddWithValue("@Birthday", friend.Birthday);
         
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return View(await _context.Friend.ToListAsync());
        }

        #endregion

        #region
        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            string connectionString = "Server=tcp:vikserver.database.windows.net,1433;Initial Catalog=vikdatabase;Persist Security Info=False;User ID=vikmcr;Password=*Fluzudo12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "DeleteFriend";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return View(await _context.Friend.ToListAsync());
        }

        private bool FriendExists(Guid id)
        {
            return _context.Friend.Any(e => e.Id == id);
        }

        #endregion
    }
}
