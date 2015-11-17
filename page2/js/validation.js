// JavaScript Document
$.ajaxSetup({  
    async : false  
});
$(document).ready(function (e) {
    function CheckSetting() {
        //document.location.href("/login.html");
        //parent.location.href("login.html");
        //window.location.href="/login.html";
        //window.location.href="login.html";
        //window.top.location.href = "login.html";
        //return;
        var t1 = $('#user_name').val();
        var t2 = $.md5($('#inputpassword').val());
        var t3 = $('#select_ques').val();
        var t4 = $.md5($('#answer').val());
        var t5 = $('#id_num').val();
        var status;
        $.post("http://hkbuebank.azurewebsites.net/api/signup/CreateUser",
               { Name: t1,
                   Password: t2,
                   Question_Id: t3,
                   Question_Answer: t4,
                   Hk_Id: t5
               },
                                                          function (data) {
                                                              status = data;
                                                              if (data == true) {
                                                                  //alert("Register Success");
                                                                  //var test = window.location;
                                                                  return;

                                                              }
                                                              if (data == false) {
                                                                  //alert("Register False");
                                                                  //window.location.href = "index.html";
                                                                  return;
                                                              }
                                                          });

        if (status == true) {
            window.open("login.html");
            return;
        }
    }
    var code = ""; //验证码
    var num = 4; //验证码位数
    var flag = 0;
    var codeList = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'); //验证码内容
    //循环获取每一位验证码
    for (var i = 0; i < num; i++) {
        //随机数 * 验证码候选元素数量 = 候选元素数组下标
        code += codeList[Math.floor(Math.random() * codeList.length)];
    }
    $("#validcode").val(code);
    $("#log").click(function (e) {
        //CheckSetting();
        var check = $("#radio_click")[0].checked;
        if (check == false) {
            $("#spanalert").css("display", "block");
            $("#spanalert label").html("Please Agree the Login Notes");
            return false;
        }

        if ($("#inputcode").val() != code) {
            $("#spanalert").css("display", "block");
            $("#spanalert label").html("CAPTCHA you entered was not correct. Please try again. ");
            return false;
        }
        if ($("#inputpassword").val() != $("#inputconfirmpassword").val()) {
            $("#spanalert").css("display", "block");
            $("#spanalert label").html("The passwords you typed do not match");
            return false;
        }
        if ($("#select_ques").val() == "0") {
            $("#spanalert").css("display", "block");
            $("#spanalert label").html("Please select your Question");
            return false;
        }
        if ($("#id_num").val() == "") {
            $("#spanalert").css("display", "block");
            $("#spanalert label").html("Please Input your Id");
            return false;
        }
        CheckSetting();



    });
    $("a[class='myclose']").click(function (e) {
        $("#spanalert").css("display", "none");
    });
});