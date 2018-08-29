using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class Account
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public Dictionary<string, List<string>> AccessControllerByActions { get; set; }
        public AccessLevels AccessLevel { get; set; }
        public string ErrorMessage { get; set; }
        public string ControllersByAction
        {
            get
            {
                if (this.AccessControllerByActions.Count > 0)
                {
                    //Ex: "Home=;UmiInstance=Create,Edit,Delete
                    string obj = string.Empty;
                    foreach (var keyValue in this.AccessControllerByActions)
                    {
                        obj += $"{keyValue.Key}=";
                        foreach (var action in keyValue.Value)
                            obj += $"{action},";
                        if (obj.Substring(obj.Length - 1, 1) == ",")
                            obj = obj.Substring(0, obj.Length - 1); //remove ,
                        obj += ";";
                    }

                    obj = obj.Substring(0, obj.Length - 1); //remove ;
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }
        public void SetControllersByAction(string obj)
        {
            this.AccessControllerByActions = new Dictionary<string, List<string>>();
            try
            {
                var controllersByActions = obj.Split(';');
                foreach (var cntrlByAct in controllersByActions)
                {
                    var access = cntrlByAct.Split('='); //[0] = controller, [1]= actions
                    List<string> actions = new List<string>();
                    if (!string.IsNullOrEmpty(access[1]))
                        foreach (var action in access[1].Split(','))
                            actions.Add(action);

                    this.AccessControllerByActions.Add(access[0], actions);
                }
            }
            catch { }
        }
        public enum AccessLevels
        {
            INVALID = 0,
            UNALLOWED = 1,
            ADMINISTRADOR = 2,
            PLANEJADOR = 3,
            SUPORTE = 4,
            MANUTENTOR = 5
        }
        public List<string> Permissions
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ControllersByAction))
                {
                    List<string> permissions = new List<string>();
                    try
                    {
                        var controllersByActions = ControllersByAction.Split(';');

                        foreach (var cntrlByAct in controllersByActions)
                        {
                            var perm = cntrlByAct.Split('='); //[0] = controller, [1]= actions
                            permissions.Add(perm[0]);
                        }

                        return permissions;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}