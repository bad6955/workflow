﻿/*!
 * jQuery demo: https://formbuilder.online/
 * Version: 2.10.9
 * Author: Kevin Chappell <kevin.b.chappell@gmail.com>
 */
! function (e) {
    "use strict";
    ! function (e) {
        var t = {};

        function r(n) {
            if (t[n]) return t[n].exports;
            var o = t[n] = {
                i: n,
                l: !1,
                exports: {}
            };
            return e[n].call(o.exports, o, o.exports, r), o.l = !0, o.exports
        }
        r.m = e, r.c = t, r.d = function (e, t, n) {
            r.o(e, t) || Object.defineProperty(e, t, {
                enumerable: !0,
                get: n
            })
        }, r.r = function (e) {
            "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {
                value: "Module"
            }), Object.defineProperty(e, "__esModule", {
                value: !0
            })
        }, r.t = function (e, t) {
            if (1 & t && (e = r(e)), 8 & t) return e;
            if (4 & t && "object" == typeof e && e && e.__esModule) return e;
            var n = Object.create(null);
            if (r.r(n), Object.defineProperty(n, "default", {
                enumerable: !0,
                value: e
            }), 2 & t && "string" != typeof e)
                for (var o in e) r.d(n, o, function (t) {
                    return e[t]
                }.bind(null, o));
            return n
        }, r.n = function (e) {
            var t = e && e.__esModule ? function () {
                return e.default
            } : function () {
                return e
            };
            return r.d(t, "a", t), t
        }, r.o = function (e, t) {
            return Object.prototype.hasOwnProperty.call(e, t)
        }, r.p = "", r(r.s = 29)
    }({
        1: function (t, r, n) {
            r.__esModule = !0;
            var o = Object.assign || function (e) {
                for (var t = 1; t < arguments.length; t++) {
                    var r = arguments[t];
                    for (var n in r) Object.prototype.hasOwnProperty.call(r, n) && (e[n] = r[n])
                }
                return e
            },
                a = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (e) {
                    return typeof e
                } : function (e) {
                    return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                },
                i = function () {
                    return function (e, t) {
                        if (Array.isArray(e)) return e;
                        if (Symbol.iterator in Object(e)) return function (e, t) {
                            var r = [],
                                n = !0,
                                o = !1,
                                a = void 0;
                            try {
                                for (var i, l = e[Symbol.iterator](); !(n = (i = l.next()).done) && (r.push(i.value), !t || r.length !== t); n = !0);
                            } catch (e) {
                                o = !0, a = e
                            } finally {
                                try {
                                    !n && l.return && l.return()
                                } finally {
                                    if (o) throw a
                                }
                            }
                            return r
                        }(e, t);
                        throw new TypeError("Invalid attempt to destructure non-iterable instance")
                    }
                }();

            function l(e, t) {
                var r = {};
                for (var n in e) t.indexOf(n) >= 0 || Object.prototype.hasOwnProperty.call(e, n) && (r[n] = e[n]);
                return r
            }
            window.fbLoaded = {
                js: [],
                css: []
            }, window.fbEditors = {
                quill: {},
                tinymce: {}
            };
            var s = r.trimObj = function (e) {
                var t = [null, void 0, "", !1, "false"];
                for (var r in e) t.includes(e[r]) ? delete e[r] : Array.isArray(e[r]) && (e[r].length || delete e[r]);
                return e
            },
                u = r.validAttr = function (e) {
                    return !["values", "enableOther", "other", "label", "subtype"].includes(e)
                },
                c = (r.xmlAttrString = function (e) {
                    return Object.entries(e).map(function (e) {
                        var t = i(e, 2),
                            r = t[0],
                            n = t[1];
                        return p(r) + '="' + n + '"'
                    }).join(" ")
                }, r.attrString = function (e) {
                    return Object.entries(e).map(function (e) {
                        var t = i(e, 2),
                            r = t[0],
                            n = t[1];
                        return u(r) && Object.values(f(r, n)).join("")
                    }).filter(Boolean).join(" ")
                }),
                f = r.safeAttr = function (e, t) {
                    e = d(e);
                    var r = void 0;
                    return t && (Array.isArray(t) ? r = E(t.join(" ")) : ("boolean" == typeof t && (t = t.toString()), r = E(t.trim()))), {
                        name: e,
                        value: t = t ? '="' + r + '"' : ""
                    }
                },
                d = (r.flattenArray = function e(t) {
                    return t.reduce(function (t, r) {
                        return t.concat(Array.isArray(r) ? e(r) : r)
                    }, [])
                }, r.safeAttrName = function (e) {
                    return {
                        className: "class"
                    }[e] || p(e)
                }),
                p = r.hyphenCase = function (e) {
                    return (e = (e = e.replace(/[^\w\s\-]/gi, "")).replace(/([A-Z])/g, function (e) {
                        return "-" + e.toLowerCase()
                    })).replace(/\s/g, "-").replace(/^-+/g, "")
                },
                m = r.camelCase = function (e) {
                    return e.replace(/-([a-z])/g, function (e, t) {
                        return t.toUpperCase()
                    })
                },
                b = r.bindEvents = function (e, t) {
                    if (t) {
                        var r = function (r) {
                            t.hasOwnProperty(r) && e.addEventListener(r, function (e) {
                                return t[r](e)
                            })
                        };
                        for (var n in t) r(n)
                    }
                },
                y = r.nameAttr = function (e) {
                    var t = (new Date).getTime();
                    return (e.type || p(e.label)) + "-" + t
                },
                v = r.getContentType = function (e) {
                    return void 0 === e ? e : [
                        ["array", function (e) {
                            return Array.isArray(e)
                        }],
                        ["node", function (e) {
                            return e instanceof window.Node || e instanceof window.HTMLElement
                        }],
                        ["component", function () {
                            return e && e.dom
                        }],
                        [void 0 === e ? "undefined" : a(e), function () {
                            return !0
                        }]
                    ].find(function (t) {
                        return t[1](e)
                    })[0]
                },
                g = function e(t) {
                    var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "",
                        n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : {},
                        o = v(r),
                        a = n.events,
                        i = l(n, ["events"]),
                        s = document.createElement(t),
                        u = {
                            string: function (e) {
                                s.innerHTML += e
                            },
                            object: function (t) {
                                var r = t.tag,
                                    n = t.content,
                                    o = l(t, ["tag", "content"]);
                                return s.appendChild(e(r, n, o))
                            },
                            node: function (e) {
                                return s.appendChild(e)
                            },
                            array: function (e) {
                                for (var t = 0; t < e.length; t++) o = v(e[t]), u[o](e[t])
                            },
                            function: function (e) {
                                e = e(), o = v(e), u[o](e)
                            },
                            undefined: function () { }
                        };
                    for (var c in i)
                        if (i.hasOwnProperty(c)) {
                            var f = d(c),
                                p = Array.isArray(i[c]) ? B(i[c].join(" ").split(" ")).join(" ") : i[c];
                            s.setAttribute(f, p)
                        }
                    return r && u[o](r), b(s, a), s
                };
            r.markup = g;
            var h = r.parseAttrs = function (e) {
                var t = e.attributes,
                    r = {};
                return j(t, function (e) {
                    var n = t[e].value || "";
                    n.match(/false|true/g) ? n = "true" === n : n.match(/undefined/g) && (n = void 0), n && (r[m(t[e].name)] = n)
                }), r
            },
                w = r.parseOptions = function (e) {
                    for (var t = [], r = 0; r < e.length; r++) {
                        var n = o({}, h(e[r]), {
                            label: e[r].textContent
                        });
                        t.push(n)
                    }
                    return t
                },
                A = r.parseXML = function (e) {
                    var t = (new window.DOMParser).parseFromString(e, "text/xml"),
                        r = [];
                    if (t)
                        for (var n = t.getElementsByTagName("field"), o = 0; o < n.length; o++) {
                            var a = h(n[o]),
                                i = n[o].getElementsByTagName("option");
                            i && i.length && (a.values = w(i)), r.push(a)
                        }
                    return r
                },
                x = r.parsedHtml = function (e) {
                    var t = document.createElement("textarea");
                    return t.innerHTML = e, t.textContent
                },
                S = r.escapeHtml = function (e) {
                    var t = document.createElement("textarea");
                    return t.textContent = e, t.innerHTML
                },
                E = r.escapeAttr = function (e) {
                    var t = {
                        '"': "&quot;",
                        "&": "&amp;",
                        "<": "&lt;",
                        ">": "&gt;"
                    };
                    return "string" == typeof e ? e.replace(/["&<>]/g, function (e) {
                        return t[e] || e
                    }) : e
                },
                O = r.escapeAttrs = function (e) {
                    for (var t in e) e.hasOwnProperty(t) && (e[t] = E(e[t]));
                    return e
                },
                j = r.forEach = function (e, t, r) {
                    for (var n = 0; n < e.length; n++) t.call(r, n, e[n])
                },
                B = r.unique = function (e) {
                    return e.filter(function (e, t, r) {
                        return r.indexOf(e) === t
                    })
                },
                T = r.removeFromArray = function (e, t) {
                    var r = t.indexOf(e);
                    r > -1 && t.splice(r, 1)
                },
                D = r.getScripts = function (e, t) {
                    var r, n = jQuery,
                        o = [];
                    return Array.isArray(e) || (e = [e]), C(e) || (o = jQuery.map(e, function (e) {
                        var r = {
                            dataType: "script",
                            cache: !0,
                            url: (t || "") + e
                        };
                        return jQuery.ajax(r).done(function () {
                            return window.fbLoaded.js.push(e)
                        })
                    })), o.push(jQuery.Deferred(function (e) {
                        return n(e.resolve)
                    })), (r = jQuery).when.apply(r, o)
                },
                C = r.isCached = function (e) {
                    var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "js",
                        r = !1,
                        n = window.fbLoaded[t];
                    return r = Array.isArray(e) ? e.every(function (e) {
                        return n.includes(e)
                    }) : n.includes(e), r
                },
                N = r.getStyles = function (t, r) {
                    Array.isArray(t) || (t = [t]), t.forEach(function (t) {
                        var n = "href",
                            o = t,
                            i = "";
                        if ("object" == (void 0 === t ? "undefined" : a(t)) && (n = t.type || (t.style ? "inline" : "href"), i = t.id, t = "inline" == n ? t.style : t.href, o = i || t.href || t.style), !C(o, "css")) {
                            if ("href" == n) {
                                var l = document.createElement("link");
                                l.type = "text/css", l.rel = "stylesheet", l.href = (r || "") + t, document.head.appendChild(l)
                            } else e('<style type="text/css">' + t + "</style>").attr("id", i).appendTo(e(document.head));
                            window.fbLoaded.css.push(o)
                        }
                    })
                },
                F = r.capitalize = function (e) {
                    return e.replace(/\b\w/g, function (e) {
                        return e.toUpperCase()
                    })
                },
                U = r.merge = function e(t, r) {
                    var n = Object.assign({}, t, r);
                    for (var o in r) n.hasOwnProperty(o) && (Array.isArray(r[o]) ? n[o] = Array.isArray(t[o]) ? B(t[o].concat(r[o])) : r[o] : "object" === a(r[o]) ? n[o] = e(t[o], r[o]) : n[o] = r[o]);
                    return n
                },
                k = r.addEventListeners = function (e, t, r) {
                    return t.split(" ").forEach(function (t) {
                        return e.addEventListener(t, r, !1)
                    })
                },
                L = r.closest = function (e, t) {
                    for (var r = t.replace(".", "");
                        (e = e.parentElement) && !e.classList.contains(r););
                    return e
                },
                M = r.mobileClass = function () {
                    var e = "";
                    return function (t) {
                        /(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(t) && (e = "fb-mobile")
                    }(navigator.userAgent || navigator.vendor || window.opera), e
                },
                I = r.safename = function (e) {
                    return e.replace(/\s/g, "-").replace(/[^a-zA-Z0-9[\]_-]/g, "")
                },
                R = r.forceNumber = function (e) {
                    return e.replace(/[^0-9]/g, "")
                },
                q = r.subtract = function (e, t) {
                    return t.filter(function (e) {
                        return !~this.indexOf(e)
                    }, e)
                },
                W = (r.insertStyle = function (e) {
                    var t = (e = Array.isArray(e) ? e : [e]).map(function (e) {
                        var t = e.src,
                            r = e.id;
                        return new Promise(function (e, n) {
                            if (window.fbLoaded.css.includes(t)) return e(t);
                            var o = g("link", null, {
                                href: t,
                                rel: "stylesheet",
                                id: r
                            });
                            document.head.insertBefore(o, document.head.firstChild)
                        })
                    });
                    return Promise.all(t)
                }, r.removeStyle = function (e) {
                    var t = document.getElementById(e);
                    return t.parentElement.removeChild(t)
                }, {
                        addEventListeners: k,
                        attrString: c,
                        camelCase: m,
                        capitalize: F,
                        closest: L,
                        getContentType: v,
                        escapeAttr: E,
                        escapeAttrs: O,
                        escapeHtml: S,
                        forceNumber: R,
                        forEach: j,
                        getScripts: D,
                        getStyles: N,
                        hyphenCase: p,
                        isCached: C,
                        markup: g,
                        merge: U,
                        mobileClass: M,
                        nameAttr: y,
                        parseAttrs: h,
                        parsedHtml: x,
                        parseOptions: w,
                        parseXML: A,
                        removeFromArray: T,
                        safeAttr: f,
                        safeAttrName: d,
                        safename: I,
                        subtract: q,
                        trimObj: s,
                        unique: B,
                        validAttr: u
                    });
            r.default = W
        },
        26: function (t, r, n) {
            r.__esModule = !0;
            var o = document.getElementById("currentFieldId");
            r.builderActions = {
                showData: function () {
                    return e(".build-wrap").formBuilder("showData")
                },
                clearFields: function () {
                    return e(".build-wrap").formBuilder("clearFields")
                },
                getData: function () {
                    console.log(e(".build-wrap").formBuilder("getData"))
                },
                setData: function () {
                    e(".build-wrap").formBuilder("setData", '[{"type":"text","label":"Full Name","subtype":"text","className":"form-control","name":"text-1476748004559"},{"type":"select","label":"Occupation","className":"form-control","name":"select-1476748006618","values":[{"label":"Street Sweeper","value":"option-1","selected":true},{"label":"Moth Man","value":"option-2"},{"label":"Chemist","value":"option-3"}]},{"type":"textarea","label":"Short Bio","rows":"5","className":"form-control","name":"textarea-1476748007461"}]')
                },
                addField: function () {
                    var t = {
                        type: "text",
                        class: "form-control",
                        label: "Text Field added at: " + (new Date).getTime()
                    };
                    e(".build-wrap").formBuilder("addField", t)
                },
                removeField: function () {
                    var t = o.value;
                    e(".build-wrap").formBuilder("removeField", t)
                },
                getXML: function () {
                    alert(e(".build-wrap").formBuilder("getData", "xml"))
                },
                getJSON: function () {
                    alert(e(".build-wrap").formBuilder("getData", "json", !0))
                },
                getJS: function () {
                    alert("check console"), console.log(e(".build-wrap").formBuilder("getData"))
                },
                toggleEdit: function () {
                    e(".build-wrap").formBuilder("toggleFieldEdit", o.value)
                },
                toggleAllEdit: function () {
                    return e(".build-wrap").formBuilder("toggleAllFieldEdit")
                },
                getFieldTypes: function () {
                    return console.log(e(".build-wrap").formBuilder("getFieldTypes"))
                }
            }, r.renderActions = {
                loadUserForm: function () {
                    e(".render-wrap").formRender({
                        controlConfig: {
                            "textarea.tinymce": {
                                branding: !1,
                                encoding: "xml",
                                menubar: "edit insert format table",
                                plugins: "preview searchreplace autolink link table lists textcolor colorpicker",
                                toolbar: "formatselect | bold italic forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | preview"
                            }
                        },
                        formData: '[{"type":"autocomplete","label":"Autocomplete","className":"form-control","name":"autocomplete-1526094918549","requireValidOption":true,"values":[{"label":"Option 1","value":"option-1"},{"label":"Option 2","value":"option-2"},{"label":"Option 3","value":"option-3"}],"userData":["option-1"]},{"type":"checkbox-group","label":"Checkbox Group","name":"checkbox-group-1526095813035","other":true,"values":[{"label":"Option 1","value":"option-1"},{"label":"Option 2","value":"option-2"}],"userData":["option-1","Bilbo \\"baggins\\""]},{"type":"text","label":"Color Field","name":"text-1526099104236","subtype":"color","userData":["#00ff00"]},{"type":"text","label":"Text Field","name":"text-1526099104236","subtype":"tel","userData":["123-456-7890"]},{"type":"date","label":"Date Field","className":"form-control","name":"date-1526096579821","userData":["2018-01-01"]},{"type":"number","label":"Number","className":"form-control","name":"number-1526099204594","min":"1","max":"3","step":".2","userData":["1.1"]},{"type":"textarea","label":"Text Area","className":"form-control","name":"textarea-1526099273610","subtype":"textarea","userData":["Tennessee Welcomes You!"]},{"type":"textarea","subtype":"tinymce","label":"TinyMCE","className":"form-control","name":"textarea-1526099273610","userData":["&lt;p&gt;&lt;span style=&quot;color: #339966;&quot;&gt;It&#39;s a great place&lt;/span&gt;&lt;/p&gt;"]}]'
                    })
                },
                clearUserForm: function () {
                    e(".render-wrap").formRender("clear")
                },
                renderUserForm: function () {
                    e(".render-wrap").formRender("render", '[{"type":"text","label":"Color picker","name":"text-1526099104236","subtype":"color","userData":["#00ff00"]},{"type":"text","label":"Text Field","name":"text-1526099104236","subtype":"tel","userData":["123-456-7890"]},{"type":"date","label":"Date Field","className":"form-control","name":"date-1526096579821","userData":["2018-01-01"]},{"type":"number","label":"Number","className":"form-control","name":"number-1526099204594","min":"1","max":"3","step":".2","userData":["1.1"]},{"type":"textarea","label":"Text Area","className":"form-control","name":"textarea-1526099273610","subtype":"textarea","userData":["Tennessee Welcomes You!"]},{"type":"textarea","subtype":"tinymce","label":"TinyMCE","className":"form-control","name":"textarea-1526099273610","userData":["&lt;p&gt;&lt;span style=&quot;color: #339966;&quot;&gt;It&#39;s a great place&lt;/span&gt;&lt;/p&gt;"]}]')
                },
                getHTML: function () {
                    console.log(e(".render-wrap").formRender("html"))
                },
                showUserData: function () {
                    alert(JSON.stringify(e(".render-wrap").formRender("userData")))
                }
            }, r.demoActions = {
                testSubmit: function () {
                    var e = new FormData(document.forms[0]);
                    console.log("Can submit: ", document.forms[0].checkValidity()), console.log("FormData:", e)
                },
                resetDemo: function () {
                    window.sessionStorage.removeItem("formData"), location.reload()
                }
            }
        },
        27: function (e, t, r) {
            (e.exports = r(9)(!1)).push([e.i, "body,html{height:100%}body{background-color:#f2f2f2;background-image:url(data:image/png;base64,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);font-family:Helvetica,Helvetica Neue,Arial,sans-serif}.form-rendered .build-wrap,.form-rendered .formbuilder-actions,.form-rendered .formbuilder-title,.formrender-actions,.formrender-title,.render-wrap{display:none}.form-rendered .formrender-actions,.form-rendered .formrender-title,.form-rendered .render-wrap{display:block}", ""])
        },
        28: function (e, t, r) {
            var n = r(27);
            "string" == typeof n && (n = [
                [e.i, n, ""]
            ]);
            var o = {
                attrs: {
                    class: "formBuilder-injected-style"
                },
                sourceMap: !1,
                hmr: !0,
                transform: void 0,
                insertInto: void 0
            };
            r(8)(n, o);
            n.locals && (e.exports = n.locals)
        },
        29: function (e, t, r) {
            var n = Object.assign || function (e) {
                for (var t = 1; t < arguments.length; t++) {
                    var r = arguments[t];
                    for (var n in r) Object.prototype.hasOwnProperty.call(r, n) && (e[n] = r[n])
                }
                return e
            };
            r(28);
            for (var o = r(1), a = r(26), i = document.querySelectorAll(".demo-dataType"), l = window.sessionStorage.getItem("dataType") || "json", s = function (e) {
                var t = e.target;
                window.sessionStorage.setItem("dataType", t.value), a.demoActions.resetDemo()
            }, u = 0; u < i.length; u++) l === i[u].value && (i[u].checked = !0), i[u].addEventListener("click", s, !1);
            document.getElementById("toggleBootstrap").addEventListener("click", function (e) {
                e.target.checked ? (0, o.insertStyle)({
                    src: "https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css",
                    id: "bootstrap"
                }) : (0, o.removeStyle)("bootstrap")
            }, !1), jQuery(function (e) {
                var t, r = {
                    starRating: function (t) {
                        return {
                            field: '<span id="' + t.name + '">',
                            onRender: function () {
                                e(document.getElementById(t.name)).rateYo({
                                    rating: 3.6
                                })
                            }
                        }
                    }
                },
                    o = {
                        disabledSubtypes: {
                            text: ["password"]
                        },
                        disabledAttrs: ["placeholder", "name"],
                        dataType: l,
                        subtypes: {
                            text: ["datetime-local"]
                        },
                        onSave: function (e, t) {
                            window.sessionStorage.setItem("formData", t), u()
                        },
                        onAddField: function (e) {
                            document.getElementById("currentFieldId").value = e
                        },
                        onClearAll: function () {
                            return window.sessionStorage.removeItem("formData")
                        },
                        stickyControls: {
                            enable: !0
                        },
                        sortableControls: !0,
                        fields: [{
                            type: "autocomplete",
                            label: "Custom Autocomplete",
                            required: !0,
                            values: [{
                                label: "SQL"
                            }, {
                                label: "C#"
                            }, {
                                label: "JavaScript"
                            }, {
                                label: "Java"
                            }, {
                                label: "Python"
                            }, {
                                label: "C++"
                            }, {
                                label: "PHP"
                            }, {
                                label: "Swift"
                            }, {
                                label: "Ruby"
                            }]
                        }, {
                            label: "Star Rating",
                            attrs: {
                                type: "starRating"
                            },
                            icon: "🌟"
                        }],
                        templates: r,
                        inputSets: [{
                            label: "User Details",
                            icon: "👨",
                            name: "user-details",
                            showHeader: !0,
                            fields: [{
                                type: "text",
                                label: "First Name",
                                className: "form-control"
                            }, {
                                type: "select",
                                label: "Profession",
                                className: "form-control",
                                values: [{
                                    label: "Street Sweeper",
                                    value: "option-2",
                                    selected: !1
                                }, {
                                    label: "Brain Surgeon",
                                    value: "option-3",
                                    selected: !1
                                }]
                            }, {
                                type: "textarea",
                                label: "Short Bio:",
                                className: "form-control"
                            }]
                        }, {
                            label: "User Agreement",
                            fields: [{
                                type: "header",
                                subtype: "h3",
                                label: "Terms & Conditions",
                                className: "header"
                            }, {
                                type: "paragraph",
                                label: "Leverage agile frameworks to provide a robust synopsis for high level overviews. Iterative approaches to corporate strategy foster collaborative thinking to further the overall value proposition. Organically grow the holistic world view of disruptive innovation via workplace diversity and empowerment."
                            }, {
                                type: "paragraph",
                                label: "Bring to the table win-win survival strategies to ensure proactive domination. At the end of the day, going forward, a new normal that has evolved from generation X is on the runway heading towards a streamlined cloud solution. User generated content in real-time will have multiple touchpoints for offshoring."
                            }, {
                                type: "checkbox",
                                label: "Do you agree to the terms and conditions?"
                            }]
                        }],
                        typeUserDisabledAttrs: {
                            autocomplete: ["access"]
                        },
                        typeUserAttrs: {
                            text: {
                                shape: {
                                    label: "Class",
                                    multiple: !0,
                                    options: {
                                        "red form-control": "Red",
                                        "green form-control": "Green",
                                        "blue form-control": "Blue"
                                    },
                                    style: "border: 1px solid red"
                                },
                                readonly: {
                                    label: "readonly",
                                    value: !1
                                }
                            }
                        },
                        disableInjectedStyle: !1,
                        actionButtons: [{
                            id: "smile",
                            className: "btn btn-success",
                            label: "😁",
                            type: "button",
                            events: {
                                click: function () {
                                    alert("😁😁😁 !SMILE! 😁😁😁")
                                }
                            }
                        }, "save"],
                        disableFields: ["autocomplete", "custom-tinymce"],
                        replaceFields: [{
                            type: "textarea",
                            subtype: "tinymce",
                            datatype: "custom-tinymce",
                            label: "tinyMCE",
                            required: !0
                        }],
                        disabledFieldButtons: {
                            text: ["copy"]
                        },
                        controlPosition: "right",
                        i18n: {
                            override: (t = {}, t["en-US"] = {
                                number: "Big Numbers"
                            }, t)
                        }
                    },
                    i = window.sessionStorage.getItem("formData"),
                    s = !0;

                function u() {
                    if (document.body.classList.toggle("form-rendered", s), s) {
                        var t = e(".build-wrap").formBuilder("getData", l);
                        e(".render-wrap").formRender({
                            formData: t,
                            templates: r,
                            dataType: l
                        }), window.sessionStorage.setItem("formData", t)
                    } else e(".build-wrap").formBuilder("setData", e(".render-wrap").formRender("userData"));
                    return s = !s
                }
                i && (o.formData = i), e(".build-wrap").formBuilder(o).promise.then(function (e) {
                    var t = n({}, a.builderActions, a.renderActions, a.demoActions);
                    Object.keys(t).forEach(function (e) {
                        document.getElementById(e).addEventListener("click", function (r) {
                            t[e]()
                        })
                    }), document.querySelectorAll(".editForm").forEach(function (e) {
                        return e.addEventListener("click", u)
                    }, !1);
                    var r = document.getElementById("setLanguage"),
                        o = window.sessionStorage.getItem("formBuilder-locale");
                    o && "en-US" !== o && (r.value = o, e.actions.setLang(o)), r.addEventListener("change", function (t) {
                        var r = t.target.value;
                        window.sessionStorage.setItem("formBuilder-locale", r), e.actions.setLang(r)
                    }, !1)
                })
            })
        },
        7: function (e, t) {
            e.exports = function (e) {
                var t = "undefined" != typeof window && window.location;
                if (!t) throw new Error("fixUrls requires window.location");
                if (!e || "string" != typeof e) return e;
                var r = t.protocol + "//" + t.host,
                    n = r + t.pathname.replace(/\/[^\/]*$/, "/");
                return e.replace(/url\s*\(((?:[^)(]|\((?:[^)(]+|\([^)(]*\))*\))*)\)/gi, function (e, t) {
                    var o, a = t.trim().replace(/^"(.*)"$/, function (e, t) {
                        return t
                    }).replace(/^'(.*)'$/, function (e, t) {
                        return t
                    });
                    return /^(#|data:|http:\/\/|https:\/\/|file:\/\/\/|\s*$)/i.test(a) ? e : (o = 0 === a.indexOf("//") ? a : 0 === a.indexOf("/") ? r + a : n + a.replace(/^\.\//, ""), "url(" + JSON.stringify(o) + ")")
                })
            }
        },
        8: function (e, t, r) {
            var n = {},
                o = function (e) {
                    var t;
                    return function () {
                        return void 0 === t && (t = e.apply(this, arguments)), t
                    }
                }(function () {
                    return window && document && document.all && !window.atob
                }),
                a = function (e) {
                    var t = {};
                    return function (e) {
                        if ("function" == typeof e) return e();
                        if (void 0 === t[e]) {
                            var r = function (e) {
                                return document.querySelector(e)
                            }.call(this, e);
                            if (window.HTMLIFrameElement && r instanceof window.HTMLIFrameElement) try {
                                r = r.contentDocument.head
                            } catch (e) {
                                r = null
                            }
                            t[e] = r
                        }
                        return t[e]
                    }
                }(),
                i = null,
                l = 0,
                s = [],
                u = r(7);

            function c(e, t) {
                for (var r = 0; r < e.length; r++) {
                    var o = e[r],
                        a = n[o.id];
                    if (a) {
                        a.refs++;
                        for (var i = 0; i < a.parts.length; i++) a.parts[i](o.parts[i]);
                        for (; i < o.parts.length; i++) a.parts.push(y(o.parts[i], t))
                    } else {
                        var l = [];
                        for (i = 0; i < o.parts.length; i++) l.push(y(o.parts[i], t));
                        n[o.id] = {
                            id: o.id,
                            refs: 1,
                            parts: l
                        }
                    }
                }
            }

            function f(e, t) {
                for (var r = [], n = {}, o = 0; o < e.length; o++) {
                    var a = e[o],
                        i = t.base ? a[0] + t.base : a[0],
                        l = {
                            css: a[1],
                            media: a[2],
                            sourceMap: a[3]
                        };
                    n[i] ? n[i].parts.push(l) : r.push(n[i] = {
                        id: i,
                        parts: [l]
                    })
                }
                return r
            }

            function d(e, t) {
                var r = a(e.insertInto);
                if (!r) throw new Error("Couldn't find a style target. This probably means that the value for the 'insertInto' parameter is invalid.");
                var n = s[s.length - 1];
                if ("top" === e.insertAt) n ? n.nextSibling ? r.insertBefore(t, n.nextSibling) : r.appendChild(t) : r.insertBefore(t, r.firstChild), s.push(t);
                else if ("bottom" === e.insertAt) r.appendChild(t);
                else {
                    if ("object" != typeof e.insertAt || !e.insertAt.before) throw new Error("[Style Loader]\n\n Invalid value for parameter 'insertAt' ('options.insertAt') found.\n Must be 'top', 'bottom', or Object.\n (https://github.com/webpack-contrib/style-loader#insertat)\n");
                    var o = a(e.insertInto + " " + e.insertAt.before);
                    r.insertBefore(t, o)
                }
            }

            function p(e) {
                if (null === e.parentNode) return !1;
                e.parentNode.removeChild(e);
                var t = s.indexOf(e);
                t >= 0 && s.splice(t, 1)
            }

            function m(e) {
                var t = document.createElement("style");
                return void 0 === e.attrs.type && (e.attrs.type = "text/css"), b(t, e.attrs), d(e, t), t
            }

            function b(e, t) {
                Object.keys(t).forEach(function (r) {
                    e.setAttribute(r, t[r])
                })
            }

            function y(e, t) {
                var r, n, o, a;
                if (t.transform && e.css) {
                    if (!(a = t.transform(e.css))) return function () { };
                    e.css = a
                }
                if (t.singleton) {
                    var s = l++;
                    r = i || (i = m(t)), n = g.bind(null, r, s, !1), o = g.bind(null, r, s, !0)
                } else e.sourceMap && "function" == typeof URL && "function" == typeof URL.createObjectURL && "function" == typeof URL.revokeObjectURL && "function" == typeof Blob && "function" == typeof btoa ? (r = function (e) {
                    var t = document.createElement("link");
                    return void 0 === e.attrs.type && (e.attrs.type = "text/css"), e.attrs.rel = "stylesheet", b(t, e.attrs), d(e, t), t
                }(t), n = function (e, t, r) {
                    var n = r.css,
                        o = r.sourceMap,
                        a = void 0 === t.convertToAbsoluteUrls && o;
                    (t.convertToAbsoluteUrls || a) && (n = u(n));
                    o && (n += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(o)))) + " */");
                    var i = new Blob([n], {
                        type: "text/css"
                    }),
                        l = e.href;
                    e.href = URL.createObjectURL(i), l && URL.revokeObjectURL(l)
                }.bind(null, r, t), o = function () {
                    p(r), r.href && URL.revokeObjectURL(r.href)
                }) : (r = m(t), n = function (e, t) {
                    var r = t.css,
                        n = t.media;
                    n && e.setAttribute("media", n);
                    if (e.styleSheet) e.styleSheet.cssText = r;
                    else {
                        for (; e.firstChild;) e.removeChild(e.firstChild);
                        e.appendChild(document.createTextNode(r))
                    }
                }.bind(null, r), o = function () {
                    p(r)
                });
                return n(e),
                    function (t) {
                        if (t) {
                            if (t.css === e.css && t.media === e.media && t.sourceMap === e.sourceMap) return;
                            n(e = t)
                        } else o()
                    }
            }
            e.exports = function (e, t) {
                if ("undefined" != typeof DEBUG && DEBUG && "object" != typeof document) throw new Error("The style-loader cannot be used in a non-browser environment");
                (t = t || {}).attrs = "object" == typeof t.attrs ? t.attrs : {}, t.singleton || "boolean" == typeof t.singleton || (t.singleton = o()), t.insertInto || (t.insertInto = "head"), t.insertAt || (t.insertAt = "bottom");
                var r = f(e, t);
                return c(r, t),
                    function (e) {
                        for (var o = [], a = 0; a < r.length; a++) {
                            var i = r[a];
                            (l = n[i.id]).refs-- , o.push(l)
                        }
                        e && c(f(e, t), t);
                        for (a = 0; a < o.length; a++) {
                            var l;
                            if (0 === (l = o[a]).refs) {
                                for (var s = 0; s < l.parts.length; s++) l.parts[s]();
                                delete n[l.id]
                            }
                        }
                    }
            };
            var v = function () {
                var e = [];
                return function (t, r) {
                    return e[t] = r, e.filter(Boolean).join("\n")
                }
            }();

            function g(e, t, r, n) {
                var o = r ? "" : n.css;
                if (e.styleSheet) e.styleSheet.cssText = v(t, o);
                else {
                    var a = document.createTextNode(o),
                        i = e.childNodes;
                    i[t] && e.removeChild(i[t]), i.length ? e.insertBefore(a, i[t]) : e.appendChild(a)
                }
            }
        },
        9: function (e, t) {
            e.exports = function (e) {
                var t = [];
                return t.toString = function () {
                    return this.map(function (t) {
                        var r = function (e, t) {
                            var r = e[1] || "",
                                n = e[3];
                            if (!n) return r;
                            if (t && "function" == typeof btoa) {
                                var o = function (e) {
                                    return "/*# sourceMappingURL=data:application/json;charset=utf-8;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(e)))) + " */"
                                }(n),
                                    a = n.sources.map(function (e) {
                                        return "/*# sourceURL=" + n.sourceRoot + e + " */"
                                    });
                                return [r].concat(a).concat([o]).join("\n")
                            }
                            return [r].join("\n")
                        }(t, e);
                        return t[2] ? "@media " + t[2] + "{" + r + "}" : r
                    }).join("")
                }, t.i = function (e, r) {
                    "string" == typeof e && (e = [
                        [null, e, ""]
                    ]);
                    for (var n = {}, o = 0; o < this.length; o++) {
                        var a = this[o][0];
                        "number" == typeof a && (n[a] = !0)
                    }
                    for (o = 0; o < e.length; o++) {
                        var i = e[o];
                        "number" == typeof i[0] && n[i[0]] || (r && !i[2] ? i[2] = r : r && (i[2] = "(" + i[2] + ") and (" + r + ")"), t.push(i))
                    }
                }, t
            }
        }
    })
}(jQuery);