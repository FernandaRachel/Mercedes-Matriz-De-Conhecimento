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



        public tblPerfilItens GetProfileItemById(int id)
        {
            tblPerfilItens Profile;

            var query = from f in _db.tblPerfilItens
                        where f.IdPerfilItem == id
                        orderby f.Sigla ascending
                        select f;

            Profile = query.FirstOrDefault();

            return Profile;
        }

        public IEnumerable<tblPerfilItens> GetProfileItemByName(string Nome)
        {
            IEnumerable<tblPerfilItens> profileItem;
            var query = _db.tblPerfilItens
                .Where(i => i.Tipo == "T");
            profileItem = query.AsEnumerable();

            //Se o nome veio vazio traz todas Atividades
            if (Nome.Count() == 0)
                return profileItem;

            var query2 = _db.tblPerfilItens
                .Where(f => f.Sigla.Contains(Nome)).Where(i => i.Tipo == "T");

            profileItem = query2.AsEnumerable();

            return profileItem;
        }


        public IEnumerable<tblPerfilItens> GetProfileItems()
        {
            IEnumerable<tblPerfilItens> Profile;



            var query = from f in _db.tblPerfilItens
                        where f.Tipo == "T"
                        orderby f.Sigla ascending
                        select f;

            Profile = query.AsEnumerable();

            return Profile;
        }


        public tblPerfilItens CreateProfileItem(tblPerfilItens ProfileItem)
        {
            _db.tblPerfilItens.Add(ProfileItem);

            _db.SaveChanges();


            return ProfileItem;
        }

        public tblPerfilItens DeleteProfileItem(int id)
        {
            tblPerfilItens ProfileItem;

            var query = from f in _db.tblPerfilItens
                        where f.IdPerfilItem == id
                        orderby f.Sigla ascending
                        select f;

            ProfileItem = query.FirstOrDefault();

            _db.tblPerfilItens.Remove(ProfileItem);
            _db.SaveChanges();

            return ProfileItem;
        }


        public tblPerfilItens UpdateProfileItem(tblPerfilItens ProfileItem)
        {
            var trainingToUpdate = _db.tblPerfilItens.Find(ProfileItem.IdPerfilItem);
            trainingToUpdate.Sigla = ProfileItem.Sigla;
            trainingToUpdate.Descricao = ProfileItem.Descricao;


            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfProfileItemAlreadyExits(tblPerfilItens ProfileItem)
        {
            var query = from f in _db.tblPerfilItens
                        where f.Sigla == ProfileItem.Sigla
                        orderby f.Sigla ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().IdPerfilItem != ProfileItem.IdPerfilItem)
                return true;

            return false;
        }


        public IPagedList<tblPerfilItens> GetProfileItensWithPagination(int pageNumber, int quantity)
        {
            IPagedList<tblPerfilItens> Profile;

            var query = from f in _db.tblPerfilItens
                        where f.Tipo == "T"
                        orderby f.Sigla ascending
                        select f;

            Profile = query.ToPagedList(pageNumber, quantity);

            return Profile;
        }

        public IEnumerable<tblPerfilItens> GetProfileItemsByType(string type)
        {
            IEnumerable<tblPerfilItens> Profile;

            var query = from f in _db.tblPerfilItens
                        orderby f.Sigla ascending
                        where f.Tipo == "T"
                        select f;

            Profile = query.AsEnumerable();

            return Profile;
        }
    }
}