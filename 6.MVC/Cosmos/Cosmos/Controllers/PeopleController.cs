﻿using Cosmos.Dao;
using Cosmos.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cosmos.Controllers
{
    public class PeopleController : Controller
    {

        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;
        // GET: People
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM Person"));
        }

        // GET: People/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return await ShowPerson(id);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name, JobOccupation, Adult")]Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.AddPersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            return await ShowPerson(id);
        }

        private async Task<ActionResult> ShowPerson(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var person = await service.GetPersonAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: People/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            return await ShowPerson(id);
        }

        // POST: People/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) 
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
