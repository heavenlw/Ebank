function getCookie(name) {
                                    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
                                    if (arr = document.cookie.match(reg))
                                        return unescape(arr[2]);
                                    else
                                        return null;
                                }
var v_user_id = getCookie("user_id");
var v_user_name = getCookie("user_name");
var v_user_session = getCookie("session");

if(v_user_id==null || v_user_session==null)
{

    window.location("login.html");
    
}
else
{
     $.post("http://localhost:53239/api/login/CheckLogin",
                                                                                                                                 {
                                                                                                                                     Name: v_user_name,
                                                                                                                                     Id: v_user_id,
Session:v_user_session
                                                                                                                                 },
                                                                                                                                 function (data, status) {
                                           if(data!="OK")
                                           {
                                                window.location("login.html");                                                                                   }
                                                                                             
                                                                                                                                 });
}
                                