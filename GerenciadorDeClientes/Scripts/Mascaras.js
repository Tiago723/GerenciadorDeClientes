
// CONFIGURAÇÕES PARA MÁSCARA DE TELEFONE
function mascara(o, f) {
    v_obj = o;
    v_fun = f;
    setTimeout("execmascara()", 1);
}

function execmascara() {
    v_obj.value = v_fun(v_obj.value);
    setCursorPosition(v_obj.value.length); // Chama a função para definir a posição do cursor
}

function mtel(v) {
    v = v.replace(/\D/g, ""); // Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); // Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2"); // Coloca hífen entre o quarto e o quinto dígitos
    return v;
}

function id(el) {
    return document.getElementById(el);
}

window.onload = function () {
    var telefoneInput = id('telefone');

    telefoneInput.onkeyup = function () {
        mascara(this, mtel);
    };

    telefoneInput.onfocus = function () {
        setCursorPosition(this.value.length); // Define a posição do cursor quando o campo recebe foco
    };

    telefoneInput.onmouseup = function () {
        setCursorPosition(this.value.length); // Define a posição do cursor quando o campo é clicado
    };
};

function setCursorPosition(position) {
    var telefoneInput = id('telefone');

    // Define a posição do cursor no campo
    if (telefoneInput.setSelectionRange) {
        telefoneInput.focus();
        telefoneInput.setSelectionRange(position, position);
    } else if (telefoneInput.createTextRange) {
        var range = telefoneInput.createTextRange();
        range.collapse(true);
        range.moveEnd('character', position);
        range.moveStart('character', position);
        range.select();
    }
}