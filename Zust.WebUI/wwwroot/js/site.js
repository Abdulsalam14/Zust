// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$(document).ready(function () {


//        var currentcontroller = '@currentController'.toLowerCase();
//        var currentaction = '@currentAction'.toLowerCase();

    //$('.sidemenu-nav .nav-item').each(function () {

    //    console.log(currentcontroller)
    //    console.log(currentaction)
    //    var $a = $(this).find('a');

    //    // a etiketi var mı ve özniteliklere sahip mi kontrol et
    //    if ($a.length > 0 && $a.attr('asp-controller') !== undefined && $a.attr('asp-action') !== undefined) {

    //        if (currentController === menuController && currentAction === menuAction) {
    //            $(this).addClass('active');
    //        } else {
    //            $(this).removeClass('active');
    //        }
    //        console.log("aaa")
    //    }

    //});


    //$('.sidemenu-nav .nav-item').each(function () {
    //    var $a = $(this).find('a');
    //    var menucontroller = $a.attr('asp-controller');
    //    var menuaction = $a.attr('asp-action');
    //    console.log(menucontroller)
    //    console.log(menuaction)
    //    if (menucontroller !== undefined && menuaction !== undefined) {
    //        menucontroller = menucontroller.tolowercase();
    //        menuaction = menuaction.tolowercase();

    //        if (currentcontroller === menucontroller && currentaction === menuaction) {
    //            $(this).addclass('active');
    //        } else {
    //            $(this).removeclass('active');
    //        }
    //    }
    //});



    //$('.sidemenu-nav .nav-item').each(function () {
    //    var $a = $(this).find('a');
    //    var menucontroller = $a.attr('asp-controller');
    //    var menuaction = $a.attr('asp-action');

    //    console.log(menucontroller);
    //    console.log(menuaction);

    //    if (menucontroller !== undefined && menuaction !== undefined) {
    //        menucontroller = menucontroller.toLowerCase(); // toLowerCase doğru yazım
    //        menuaction = menuaction.toLowerCase(); // toLowerCase doğru yazım

    //        if (currentcontroller === menucontroller && currentaction === menuaction) {
    //            $(this).addClass('active'); // addClass doğru yazım
    //        } else {
    //            $(this).removeClass('active'); // removeClass doğru yazım
    //        }
    //    }
    //});




    //$('.sidemenu-nav .nav-item').each(function () {
    //    var menucontroller = $(this).find('a').attr('asp-controller').tolowercase();
    //    var menuaction = $(this).find('a').attr('asp-action').tolowercase();

    //    if (currentcontroller === menucontroller && currentaction === menuaction) {
    //        $(this).addclass('active');
    //    } else {
    //        $(this).removeclass('active');
    //    }
    //});
//});


//$(document).ready(function () {
//    // Hangi controller'ın aktif olduğunu al
//    //var currentController = '@Context.Request.RouteValues["controller"]'

//    //var currentController = '@(Context.Request.RouteValues["controller"] ?? "")'.toLowerCase();
//    //console.log(currentController)

//    //// Menü elemanlarını kontrol et
//    //$('.sidemenu-nav .nav-item').each(function () {
//    //    var menuController = $(this).find('a').attr('asp-controller')

//    //    // Eğer mevcut controller ile menü controller'ı eşleşiyorsa "active" sınıfını ekle
//    //    if (currentController === menuController) {
//    //        $(this).addClass('active');
//    //    }
//    //});


//    console.log('Current Controller: @currentController');
//    console.log('Current Action: @currentAction');

//    $('.sidemenu-nav .nav-item').each(function () {
//        var $a = $(this).find('a');
//        var menuController = $a.attr('asp-controller');
//        var menuAction = $a.attr('asp-action');

//        if (menuController !== undefined && menuAction !== undefined) {
//            menuController = menuController.toLowerCase();
//            menuAction = menuAction.toLowerCase();

//            if (currentController === menuController && currentAction === menuAction) {
//                $(this).addClass('active');
//            } else {
//                $(this).removeClass('active');
//            }
//        }
//    });
//});


