// per una descrizione di questa tecnica vedere:
// Jeremy Keith, Dom Scripting pag. 81.
// praticamente esegue un append di tutte le funzioni
// da eseguire quando viene caricata la pagina.
// N.B. windows.onload viene eseguita alla fine del
// caricamento della pagina.
function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != "function") {
        window.onload = func;
    }
    else {
        window.onload = function () {
            if (oldonload) {
                oldonload();
            }
            func();
        };
    }
}

addLoadEvent(function () {
    // inserire qui il codice da eseguire al caricamento della pagina
});

// chiamata al completamento del caricamento della pagina.
$(function () {
    try {
        //alert("qua");
    } catch (e) {
        alert(e.message);
    }
});


// forza il reload della pagina
function PageReload() {
    window.location.href = window.location.href;
}

$("#Add").click(function() {
    // deve leggere gli elementi selezionati della listbox dei rpodotti
    // e poi aggiungerli alla listbox contenente i prodotti per listino.
    var selcode = [];       // array contenente i codici prodotti selezionati dalla listbox
    var selid = [];         // array contnente gli id prodotti relativi ai codici selezionati
    //var s = document.getElementById("lbprodid");        // listbox degli id
    //var s1 = document.getElementById("lbproducts");     // listbox dei codici

    var s = $("#lbprodid");        // listbox degli id
    var s1 =$("#lbproducts");     // listbox dei codici

    //var s = $('#lbproducts');
    var s2 = document.getElementById("lbprodplid");     // listbox degli id relativi ai prodotti contenuti nel listino (hidden) 
    var s3 = document.getElementById("lbprodpl");       // listbox dei codici relativi ai prodotti del listino


    var numel = $("#lbproducts option").length;
    for (var i = 0; i < numel; i++) {
        if ($("#lbproducts option")[i].selected) {
            selcode.push(s1.options[i].text);
            //selid.push(s.options[i].text);
        }
    }

    //var numel = s1.options.length;
    for (var i = 0; i < numel; i++) {
        if (s1.options[i].selected) {
            selcode.push(s1.options[i].text);
            selid.push(s.options[i].text);
        }
    }
    for (var i = 0; i < selid.length; i++) {
        var option = document.createElement("option");
        option.text = selcode[i];
        s3.add(option);
        //s2.add(selid[i]);
    }
});

$("#Remove").click(function() {
    //$('#RemoveProduct').CSS('color', "#00ff00");
    alert("quo");
});

