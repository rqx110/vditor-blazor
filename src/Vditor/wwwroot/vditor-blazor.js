// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function createVditor(domRef, editor, value, options = {}) {
  options = convertJsonKey(options);

  if (options.toolbar) {
    options.toolbar.forEach(btn => {
      if (typeof btn == 'object') {
        btn.click = () => {
          editor.invokeMethodAsync('HandleToolbarButtonClick', btn.name);
        }
      }
    })
  }

  domRef.Vditor = new Vditor(domRef, {
    ...options,
    theme: 'ant-design',
    icon: 'ant',
    cache: {
      enable: false,
    },
    value,
    after: () => {
      editor.invokeMethodAsync('HandleRendered', value);
    },
    input: (value) => {
      editor.invokeMethodAsync('HandleInput', value);
    },
    focus: (value) => {
      editor.invokeMethodAsync('HandleFocus', value);
    },
    blur: (value) => {
      editor.invokeMethodAsync('HandleBlur', value);
    },
    esc: (value) => {
      editor.invokeMethodAsync('HandleEscPress', value);
    },
    ctrlEnter: (value) => {
      editor.invokeMethodAsync('HandleCtrlEnterPress', value);
    },
    select: (value) => {
      editor.invokeMethodAsync('HandleSelect', value);
    },
  });
}

export function getValue(domRef) {
  return domRef.Vditor.getValue();
}

export function getHTML(domRef) {
  return domRef.Vditor.getHTML();
}

export function setValue(domRef, value, clearStack = false) {
  domRef.Vditor.setValue(value, clearStack);
}

export function insertValue(domRef, value, render = true) {
  domRef.Vditor.insertValue(value, render);
}

export function destroy(domRef) {
  domRef.Vditor.destroy();
}

export function preview(domRef, editor, markdown, options = {}) {
  options = convertJsonKey(options);
  Vditor.preview(domRef, markdown, {
    ...options,
    after() {
      if (options.handleAfter) {
        editor.invokeMethodAsync('HandleAfter');
      }
    }
  });
}

var convertJsonKey = function (jsonObj) {
  var result = {};
  for (var key in jsonObj) {
    var keyval = jsonObj[key];
    if (keyval == null) {
      continue;
    }

    key = key.replace(key[0], key[0].toLowerCase());

    result[key] = keyval;
  }
  return result;
}