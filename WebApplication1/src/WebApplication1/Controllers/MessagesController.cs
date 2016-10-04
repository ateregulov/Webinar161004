using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Messages
        public async Task<IActionResult> MyMessages()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var applicationDbContext = _context.Messages.Where(m => m.RecipientId == userId || m.SenderId == userId);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,Datetime,RecipientId,SenderId,Text")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.SenderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                message.Datetime = DateTime.Now;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id", message.RecipientId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.SingleOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id", message.RecipientId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", message.SenderId);
            return View(message);
        }

      
       
    }
}
