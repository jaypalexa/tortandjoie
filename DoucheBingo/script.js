/*global document: false, jQuery: false */
(function ($) {
    "use strict"; 
    $.fn.DoucheBingo = function () {
        var usedNums = new Array(16),
        me = {
            initAll: function () {
                $('.bingo-square').click(function () {
                    me.clickSquare(this);
                });
                $('a#new_link').click(function () {
                    me.anotherCard();
                    return false;
                });
                $('a#clear_link').click(function () {
                    me.clearCard();
                    return false;
                });
                me.anotherCard();
            },

            newCard: function () {
                var i;
                for (i = 0; i < 16; i += 1) {
                    me.setSquare(i);
                }
            },

            setSquare: function (thisSquare) {
                var newNum, currSquare = "square_" + thisSquare,
                    colPhrase = [
                    "ADDITIONS & SUBTRACTIONS",
                    "AGILE",
                    "ALIGN",
                    "AMBASSADOR",
                    "ANTICIPATE",
                    "BOIL THE OCEAN",
                    "BREAK BREAD",
                    "DNA",
                    "FAMILY",
                    "FIRESIDE CHAT",
                    "GLUE",
                    "HEAD IN THE GAME",
                    "HUDDLED-UP",
                    "IN TANDEM",
                    "IN THE INTERESTS OF TIME",
					"ITERATE",
                    "JOIN THE DOTS",
                    "JOURNEY",
                    "MACRO",
                    "MISS A BEAT",
                    "MOVE THE NEEDLE",
                    "NAVIGATE",
                    "NOSTRADAMUS",
                    "PIN-HEAD",
                    "PROSPECTIVE",
                    "PUSH EVERY BUTTON",
                    "ROB PETER TO PAY PAUL",
                    "STREAMLINING",
                    "TERRIFIC",
                    "UNDER THE HOOD",
                    "WIN THE GAME"
				];

                do {
                    newNum = me.getNewNum();
                }
                while (usedNums[newNum]);

                usedNums[newNum] = true;
                document.getElementById(currSquare).innerHTML = colPhrase[newNum];
            },

            getNewNum: function () {
                //-- return a random integer between 0 and 29
                return Math.floor(Math.random() * 30);
            },

            anotherCard: function () {
                var i;
                for (i = 0; i < usedNums.length; i += 1) {
                    usedNums[i] = false;
                }

                me.newCard();
                return false;
            },

            clearCard: function () {
                $('.bingo-square').each(function () {
                    $(this).removeClass("selected");
                });
            },

            showDouchePopup: function () {
                $.prompt('************ DOUCHE!!! ************', {
                    buttons: { OK: true },
                    prefix: 'cleanblue',
                    top: '50%',
                    promptspeed: 'fast'
                });
            },

            clickSquare: function (thisElement) {

                if ($(thisElement).hasClass("selected")) {
                    $(thisElement).removeClass("selected");
                } else {
                    $(thisElement).addClass("selected");
                }

                if ($("td#square_0").hasClass("selected") && $("td#square_1").hasClass("selected") && $("td#square_2").hasClass("selected") && $("td#square_3").hasClass("selected")) {

                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_4").hasClass("selected") && $("td#square_5").hasClass("selected") && $("td#square_6").hasClass("selected") && $("td#square_7").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_8").hasClass("selected") && $("td#square_9").hasClass("selected") && $("td#square_10").hasClass("selected") && $("td#square_11").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_12").hasClass("selected") && $("td#square_13").hasClass("selected") && $("td#square_14").hasClass("selected") && $("td#square_15").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_0").hasClass("selected") && $("td#square_1").hasClass("selected") && $("td#square_2").hasClass("selected") && $("td#square_3").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_1").hasClass("selected") && $("td#square_5").hasClass("selected") && $("td#square_9").hasClass("selected") && $("td#square_13").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_2").hasClass("selected") && $("td#square_6").hasClass("selected") && $("td#square_10").hasClass("selected") && $("td#square_14").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_3").hasClass("selected") && $("td#square_7").hasClass("selected") && $("td#square_11").hasClass("selected") && $("td#square_15").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_0").hasClass("selected") && $("td#square_5").hasClass("selected") && $("td#square_10").hasClass("selected") && $("td#square_15").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

                if ($("td#square_3").hasClass("selected") && $("td#square_6").hasClass("selected") && $("td#square_9").hasClass("selected") && $("td#square_12").hasClass("selected")) {
                    me.showDouchePopup();
                    return false;
                }

            }

        };
        me.initAll();
    };
} (jQuery));