using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebApi {

    private static string baseUrl = "http://vrhackspace.cba.pl/";

    public static void createUserAccount(string name, string email, string password) {
        List<IMultipartFormSection> data = new List<IMultipartFormSection>();

        data.Add(new MultipartFormDataSection("name="+name));
        data.Add(new MultipartFormDataSection("email=" + email));
        data.Add(new MultipartFormDataSection("password=" + password));

        UnityWebRequest.Post(baseUrl+"createAccount.php", data);
    }

}
