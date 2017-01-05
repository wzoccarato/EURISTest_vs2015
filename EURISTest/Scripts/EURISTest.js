// per una descrizione di questa tecnica vedere:
// Jeremy Keith, Dom Scripting pag. 81.
// praticamente esegue un append di tutte le funzioni
// da eseguire quando viene caricata la pagina.
// N.B. windows.onload viene eseguita alla fine del
// caricamento della pagina.
function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
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

$('#Add').click(function() {
    //$('#AddProduct').CSS('color', "#ff0000");
    alert("qui");
});

$('#Remove').click(function() {
    //$('#RemoveProduct').CSS('color', "#00ff00");
    alert("quo");
});