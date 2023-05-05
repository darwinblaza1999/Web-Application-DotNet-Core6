
(function ($) {
    "use strict";

    /*[ Load page ]
    ===========================================================*/
    $(".animsition").animsition({
        inClass: 'fade-in',
        outClass: 'fade-out',
        inDuration: 1500,
        outDuration: 800,
        linkElement: '.animsition-link',
        loading: true,
        loadingParentElement: 'html',
        loadingClass: 'animsition-loading-1',
        loadingInner: '<div class="loader05"></div>',
        timeout: false,
        timeoutCountdown: 5000,
        onLoadEvent: true,
        browser: [ 'animation-duration', '-webkit-animation-duration'],
        overlay : false,
        overlayClass : 'animsition-overlay-slide',
        overlayParentElement : 'html',
        transition: function(url){ window.location.href = url; }
    });
    
    /*[ Back to top ]
    ===========================================================*/
    var windowH = $(window).height()/2;

    $(window).on('scroll',function(){
        if ($(this).scrollTop() > windowH) {
            $("#myBtn").css('display','flex');
        } else {
            $("#myBtn").css('display','none');
        }
    });

    $('#myBtn').on("click", function(){
        $('html, body').animate({scrollTop: 0}, 300);
    });


    /*==================================================================
    [ Fixed Header ]*/
    var headerDesktop = $('.container-menu-desktop');
    var wrapMenu = $('.wrap-menu-desktop');

    if($('.top-bar').length > 0) {
        var posWrapHeader = $('.top-bar').height();
    }
    else {
        var posWrapHeader = 0;
    }
    

    if($(window).scrollTop() > posWrapHeader) {
        $(headerDesktop).addClass('fix-menu-desktop');
        $(wrapMenu).css('top',0); 
    }  
    else {
        $(headerDesktop).removeClass('fix-menu-desktop');
        $(wrapMenu).css('top',posWrapHeader - $(this).scrollTop()); 
    }

    $(window).on('scroll',function(){
        if($(this).scrollTop() > posWrapHeader) {
            $(headerDesktop).addClass('fix-menu-desktop');
            $(wrapMenu).css('top',0); 
        }  
        else {
            $(headerDesktop).removeClass('fix-menu-desktop');
            $(wrapMenu).css('top',posWrapHeader - $(this).scrollTop()); 
        } 
    });


    /*==================================================================
    [ Menu mobile ]*/
    $('.btn-show-menu-mobile').on('click', function(){
        $(this).toggleClass('is-active');
        $('.menu-mobile').slideToggle();
    });

    var arrowMainMenu = $('.arrow-main-menu-m');

    for(var i=0; i<arrowMainMenu.length; i++){
        $(arrowMainMenu[i]).on('click', function(){
            $(this).parent().find('.sub-menu-m').slideToggle();
            $(this).toggleClass('turn-arrow-main-menu-m');
        })
    }

    $(window).resize(function(){
        if($(window).width() >= 992){
            if($('.menu-mobile').css('display') == 'block') {
                $('.menu-mobile').css('display','none');
                $('.btn-show-menu-mobile').toggleClass('is-active');
            }

            $('.sub-menu-m').each(function(){
                if($(this).css('display') == 'block') { console.log('hello');
                    $(this).css('display','none');
                    $(arrowMainMenu).removeClass('turn-arrow-main-menu-m');
                }
            });
                
        }
    });

    /*==================================================================
    [ Isotope ]*/
    var $topeContainer = $('.isotope-grid');
    var $filter = $('.filter-tope-group');
    /*==================================================================
    [ button cancel]*/
    $('#btncancel').on('click', function () {
        window.location.href = Watch_Url.view_url;
    })
    /*==================================================================
    [ Delete Item]*/
  
    $('#delete').on('click', function () {
        $('#deleteModal').modal('show');
    });

    $('#deleteItem').on('click', function () {
        var id = $('#updateID').val();
        $('#deleteModal').modal('hide');

        $.ajax({
            url: Watch_Url.del_url,
            data: { 'id': id },
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result, "result")
                if (result.response.isSuccess == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your file has been deleted.',
                        showConfirmButton: false,
                        timer: 4000
                    })
                    window.location.href = Watch_Url.view_url;
                }
                else {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your file has been deleted.',
                        showConfirmButton: false,
                        timer: 4000
                    })
                    window.history.back();
                }
            }
        });
    });

    /*==================================================================
[ event/form validation ]*/

    $(document).ready(function () {
        $('#formupdate').submit(function (event) {
            event.preventDefault();
            if ($(this).valid()) {
                var model2 = {
                    ItemNo: $('#itemno').val(),
                    ItemName: $('#itemname').val(),
                    Price: $('#price').val(),
                    ShortDescription: $('#shortdesc').val(),
                    FullDescription: $('#fulldesc').val(),
                    Diameter: $('#diameter').val(),
                    Caliber: $('#caliber').val(),
                    Chronograph: $('#chronograph').val(),
                    Movement: $('#movement').val(),
                    Thickness: $('#thickness').val(),
                    Height: $('#height').val(),
                    Weight: $('#weight').val(),
                    Jewel: $('#jewel').val(),
                    CaseMaterial: $('#casemat').val(),
                    StrapMaterial: $('#strapmat').val(),
                    File: null,
                    Image: $('#image').val()
                };

                $.ajax({
                    url: Watch_Url.update_url,
                    cache: false,
                    data: JSON.stringify(model2),
                    type: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    success: function (result) {
                        $('.js-modal1').removeClass('show-modal1');
                        if (result.response.isSuccess == true) {

                            Swal.fire({
                                icon: 'success',
                                title: 'Your details has been successfully updated.',
                                showConfirmButton: false,
                                timer: 4000
                            })
                            window.location.href = Watch_Url.view_url;
                        }
                        else {
                            console.log(result, "test");
                            Swal.fire({
                                icon: 'error',
                                title: 'Unable to update details' + result.response.data.message,
                                showConfirmButton: false,
                                timer: 4000
                            });
                            return;
                            //window.location.href = Watch_Url.getdetails_url;
                        }
                    }
                });
            }
            else {
                return;
            }
        });

        //disabled button
        $('input:file').change(
            function () {
                if ($(this).val()) {
                    $('input:submit').attr('disabled', false);
                    // $('input:submit').removeAttr('disabled');
                }
            }
        );
    /*==================================================================
[ update modal1 ]*/
    $('#btnedit').on('click', function () {
        $('.js-modal1').addClass('show-modal1');
    });
    /*==================================================================
    [ Show modal1 ]*/
    $('.js-hide-modal1').on('click',function(){
        $('.js-modal1').removeClass('show-modal1');
    });
    /*==================================================================
[ Event textbox]*/
    $('#id').on('input', function () {
        this.value = this.value.match(/^\d+\.?\d{0,2}/);
    });

    /*==================================================================
[ Update details]*/
    
    });

})(jQuery);