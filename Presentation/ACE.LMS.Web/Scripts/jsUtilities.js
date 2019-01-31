var getTheme = function () {
    return "ui-sunny";
}

jQuery.fn.blockElement = function (settings) {
    var optBlockUi = {
        showImage: false
    };

    if (settings) { $.extend(optBlockUi, settings); }
    if (settings.showImage) {
        $(this).block({
            css: {
                border: '1px solid #000',
                padding: '0px',
                backgroundColor: '#fff', '-webkit-border-radius': '2px', '-moz-border-radius': '2px',
                opacity: .9,
                color: '#6B6F73',
                //top: '50%',
                //left: '50%',
                //textAlign: 'center',
                right: '',
                width: '100px',
                cursor: 'arrow'
            },
            message: '<span><img src="/Content/Images/ajax-loader.gif" /><b>Loading...</b></span>',
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.3,
                cursor: 'arrow'
            },
        });

    } else {
        $(this).block({
            css: {
                border: 'none',
                padding: '15px',
                //backgroundColor: '#000','-webkit-border-radius': '10px','-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff',
                top: '50%',
                left: '50%',
                textAlign: 'center',
                right: ''
            },
            message: null
        });
    }

};

function displayPopupNotification(message, messagetype, modal) {
    //types: success, error
    var container;
    var window;
    if (messagetype == 'success') {
        //success
        container = $('#dialog-notifications-success-container');
        window = $('#dialog-notifications-success');
    }
    else if (messagetype == 'error') {
        //error
        container = $('#dialog-notifications-error-container');
        window = $('#dialog-notifications-error');
    }
    else {
        //other
        container = $('#dialog-notifications-success-container');
        window = $('#dialog-notifications-success');
    }

    //we do not encode displayed message
    var htmlcode = '';
    if ((typeof message) == 'string') {
        htmlcode = '<p>' + message + '</p>';
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p>' + message[i] + '</p>';
        }
    }

    container.html(htmlcode);

    var isModal = (modal ? true : false);
    //container.dialog({ modal: isModal });
    window.jqxWindow({ isModal: true, theme: getTheme() });
    window.jqxWindow('open');
}

var barNotificationTimeout;
function displayBarNotification(message, messagetype, timeout) {
    clearTimeout(barNotificationTimeout);

    //types: success, error
    var cssclass = 'success';
    if (messagetype == 'success') {
        cssclass = 'success';
    }
    else if (messagetype == 'error') {
        cssclass = 'error';
    }
    //remove previous CSS classes and notifications
    $('#bar-notification')
        .removeClass('success')
        .removeClass('error');
    $('#bar-notification .content').remove();

    //we do not encode displayed message

    //add new notifications
    var htmlcode = '';
    if ((typeof message) == 'string') {
        htmlcode = '<p class="content">' + message + '</p>';
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p class="content">' + message[i] + '</p>';
        }
    }
    $('#bar-notification-container').append(htmlcode);
    $('#bar-notification').jqxNotification({ opacity: 0.9, theme: getTheme(), template: messagetype, appendContainer: "#container", autoClose: false });
    $('#bar-notification').jqxNotification("open");

    //$('#bar-notification').append(htmlcode)
    //    .addClass(cssclass)
    //    .fadeIn('slow')
    //    .mouseenter(function () {
    //        clearTimeout(barNotificationTimeout);
    //    });

    //$('#bar-notification .close').unbind('click').click(function () {
    //    $('#bar-notification').fadeOut('slow');
    //});

    ////timeout (if set)
    //if (timeout > 0) {
    //    barNotificationTimeout = setTimeout(function () {
    //        $('#bar-notification').fadeOut('slow');
    //    }, timeout);
    //}
}

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}

Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + parseInt(days));
    return this;
};

Date.prototype.addMonths = function (months) {
    this.setMonth(this.getMonth() + parseInt(months));
    return this;
};

function jqxConfirm(title, message, width, height, callback) {

    var jqxConfirm = $('<div><div>' + title + '</div><div><div>' + message + '</div><div style="text-align: right;"><input type="button" value="Ok" />&nbsp;&nbsp;<input type="button" value="Cancel" /></div></div></div>');
    var btnOK = $(jqxConfirm[0].children[1].children[1].children[0]);
    var btnCANCEL = $(jqxConfirm[0].children[1].children[1].children[1]);

    jqxConfirm.jqxWindow({
        theme: getTheme(),
        resizable: false,
        autoOpen: true,
        zIndex: 99999,
        isModal: true,
        width: width,
        height: height,
        okButton: $('#' + btnOK.uniqueId), //function (e) { alert(e); },
        cancelButton: $('#' + btnCANCEL.uniqueId),
        initContent: function () {
            btnOK.jqxButton({
                theme: getTheme(),
                width: 65,
                height: 25,
            }).on('click', function (e) { jqxConfirm.jqxWindow('close'); return callback(true); });

            btnCANCEL.jqxButton({
                theme: getTheme(),
                width: 65,
                height: 25,
            }).on('click', function (e) { jqxConfirm.jqxWindow('close'); return callback(false); });

            btnOK.focus();
        }
    });

    jqxConfirm.on('close', function (event) { jqxConfirm.jqxWindow('destroy'); });

    var onOkClick = function () { return callback(true); }

    //$('#' + btnOKId).click(function (e) {
    //    return callback(true);
    //});
}