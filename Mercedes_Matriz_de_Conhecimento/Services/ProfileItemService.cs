using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using PagedList;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class ProfileItemService : IProfileItemService
    {

        public DbConnection _db = new DbConnection();



        public tblPerfilItem GetProfileItemById(int id)
        {
            tblPerfilItem Profile;

            var query = from f in _db.tblPerfilItem
                        where f.IdPerfilItem == id
                        orderby f.Sigla ascending
                        select f;

            Profile = query.FirstOrDefault();

            return Profile;
        }

        public IEnumerable<tblPerfilItem> GetProfileItems()
        {
            IEnumerable<tblPerfilItem> Profile;



            var query = from f in _db.tblPerfilItem
                        orderby f.Sigla ascending
                        select f;

            Profile = query.AsEnumerable();

            return Profile;
        }


        public tblPerfilItem CreateProfileItem(tblPerfilItem ProfileItem)
        {
            _db.tblPerfilItem.Add(ProfileItem);

            _db.SaveChanges();


            return ProfileItem;
        }

        public tblPerfilItem DeleteProfileItem(int id)
        {
            tblPerfilItem ProfileItem;

            var query = from f in _db.tblPerfilItem
                        where f.IdPerfilItem == id
                        orderby f.Sigla ascending
                        select f;

            ProfileItem = query.FirstOrDefault();

            _db.tblPerfilItem.Remove(ProfileItem);
            _db.SaveChanges();

            return ProfileItem;
        }

        
        public tblPerfilItem UpdateProfileItem(tblPerfilItem ProfileItem)
        {
            var trainingToUpdate = _db.tblPerfilItem.Find(ProfileItem.IdPerfilItem);
            trainingToUpdate.Sigla = ProfileItem.Sigla;
            trainingToUpdate.Descricao = ProfileItem.Descricao;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfProfileItemAlreadyExits(tblPerfilItem ProfileItem)
        {
            var query = from f in _db.tblPerfilItem
                        where f.Sigla == ProfileItem.Sigla
                        orderby f.Sigla ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdPerfilItem != ProfileItem.IdPerfilItem)
                return true;

            return false;
        }

        public IEnumerable<tblPerfilItem> GetTrainingGroupsWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblPerfilItem> Profile;



            var query = from f in _db.tblPerfilItem
                        orderby f.Sigla ascending
                        select f;

            Profile = query.ToPagedList(pageNumber,quantity);

            return Profile;
        }
    }
}