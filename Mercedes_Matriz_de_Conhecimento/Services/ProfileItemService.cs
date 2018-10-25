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

        public IEnumerable<tblPerfilItens> GetProfileItemByName(string Nome, int idTrainProfile)
        {
            List<tblPerfilItens> profileItemTrain = new List<tblPerfilItens>();

            //Se o nome veio vazio traz todas Atividades
            if (Nome.Count() == 0)
                return GetProfileItemsNotAssociated(idTrainProfile);

            var query = _db.tblPerfilItens.Where(f => f.Tipo == "T" && f.Sigla.Contains(Nome));

            foreach (var p2 in query)
            {
                var query2 = _db.tblPerfilTreinamentoxPerfilItem
                    .Where(p => p.IdPerfilTreinamento == idTrainProfile && p.IdPerfilTreinamento == p2.IdPerfilItem);

                if (query2.Count() == 0)
                    profileItemTrain.Add(p2);
            }

            return profileItemTrain;
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

        public IEnumerable<tblPerfilItens> GetProfileItemsNotAssociated(int idTrainProfile)
        {
            List<tblPerfilItens> traingProfileItem = new List<tblPerfilItens>();

            var query = from f in _db.tblPerfilItens
                        where f.Tipo == "T"
                        orderby f.Sigla ascending
                        select f;

            foreach (var p2 in query)
            {
                var query2 = _db.tblPerfilTreinamentoxPerfilItem
                    .Where(p => p.IdPerfilTreinamento == idTrainProfile && p.IdPerfilItem == p2.IdPerfilItem);

                if (query2.Count() == 0)
                    traingProfileItem.Add(p2);
            }

            return traingProfileItem;
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