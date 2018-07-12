﻿using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.Entity;
using System.Net;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ProfileItemController : Controller
    {


        private ProfileItemService _ProfileItem;


        public ProfileItemController()
        {
            _ProfileItem = new ProfileItemService();

        }

        // GET: profileItem
        public ActionResult Index()
        {
            IEnumerable<tblPerfilItem> profileItem;
            profileItem = _ProfileItem.GetProfileItems();

            return View(profileItem);

        }

        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        public ActionResult Details(int id)
        {

            tblPerfilItem profileItem;
            profileItem = _ProfileItem.GetProfileItemById(id);

            if (profileItem == null)
                return View("Index");

            return View("Edit", profileItem);
        }


        [HttpPost]
        public ActionResult Create(tblPerfilItem profileItem)
        {
            var exits = _ProfileItem.checkIfProfileItemAlreadyExits(profileItem);

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _ProfileItem.CreateProfileItem(profileItem);

                    return RedirectToAction("Index");
                }

            }

            return View("Create", profileItem);
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilItem profileItem, int id)
        {
            profileItem.IdPerfilItem = id;
            var exits = _ProfileItem.checkIfProfileItemAlreadyExits(profileItem);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _ProfileItem.UpdateProfileItem(profileItem);

                    return RedirectToAction("Index");
                }

            }
            return View(profileItem);
        }


        // GET: profileItem/Delete/5
        public ActionResult Delete(int id)
        {

            _ProfileItem.DeleteProfileItem(id);

            return RedirectToAction("Index");

        }


    }
}