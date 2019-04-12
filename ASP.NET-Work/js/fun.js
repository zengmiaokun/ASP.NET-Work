//获取姓名并传入query_XS()方法
function button_ValidXS() {
    var cname = $("#TextBox_XM").val();
    query_XS(cname);
}

//这是一个JQuery-Ajax类型的方法
function query_XS() {
    $.ajax({
        type: 'post',
        url: 'StudentService.asmx/query_XS',
        async: true,
        data: { 'xm': name },
        success: function (result) {
            var cname = $(result).find('Xm').text();
            if (cname != "") {
                $("#TextBox_XM").val(cname);
                if ($(result).find('Xb').text() == 1)
                    $("#<%=RadioButtonList_XB.ClientID%>").find("input[type='radio']")[0].checked = true;
                else
                    $("#<%=RadioButtonList_XB.ClientID%>").find("input[type='radio']")[1].checked = true;
                $("#TextBox_CSSJ").val($(result).find('Cssj').text());
                $("#TextBox_KCS").val($(result).find('Kcs').text());
                $("#TextBox_MSG").val($(result).find('Msg').text());
            }
            else {
                $("#Label_MSG").text("");
                $("#Button_AddXs").attr("disabled", false);
            }
        },
        error: function () { }
    });
}
