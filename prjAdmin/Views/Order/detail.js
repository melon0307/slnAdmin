//篩選訂單======================================
//$("#content").load('@Url.Content("~/Order/Filter1")' + "?id=" + id );

$(".btn-group").on("click", ".btn-check", function () {
    $(":checked").each(function () {
        //console.log($(this).val());
        let id = $(this).val();
        $("#content").load('@Url.Content("~/Order/Filter1")' + "?id=" + id);

    })

});

//選單特效======================================
let pro = document.querySelector("#order");
pro.classList.add("active");
$(".key").parents("tr").css("background-color", "#FFFAF4");


//訂單明細======================================
$("tbody").on("click", "#btndetail", function () {
    console.log("ddd")
    let id = $(this).parent().parent().find("#idorderid").val();
    $("#detailbody").html("");
    $.get('@Url.Content("~/Order/Detail")', { "id": id },
        function (data) {
            let 小計 = 0;
            let 折價卷金額 = 0;
            let 運費 = 0;
            let 總金額 = 0;
            $.each(data, function (index, value) {
                let content = `<tr><td>${value.d產品名}</td><td>${value.d單價}</td><td>${value.d數量}</td><td>${value.d小計}</td>`
                $("#detailbody").append(content);
                小計 += value.d小計;
                折價卷金額 = value.d優惠卷金額;
                運費 = value.d運費;
            })
            總金額 = 小計 - 折價卷金額 + 運費;
            $("#labsubtotal").html(`商品小計 : $NT${小計}`);
            if (折價卷金額 != 0) {
                $("#labcoupon").html(`優惠折扣 : -$NT${折價卷金額}`);
            }
            else $("#labcoupon").html("");
            $("#labfee").html(`運費 : $NT${運費}`);
            $("#labtotal").html(`訂單總金額 : $NT${總金額}`).css({ "font-size": 20, "color": "#F75000" });



        })

})


//訂單狀況======================================
$("button[id='dropdownMenuButton1']").each(function () {
    if ($(this).parent().find("#stateid").val() == 1) $(this).addClass("active1");
    else if ($(this).parent().find("#stateid").val() == 2) $(this).addClass("active2");
    else if ($(this).parent().find("#stateid").val() == 3) $(this).addClass("active3");
    else if ($(this).parent().find("#stateid").val() == 4) $(this).addClass("active4");
    else if ($(this).parent().find("#stateid").val() == 5) $(this).addClass("active5");
})