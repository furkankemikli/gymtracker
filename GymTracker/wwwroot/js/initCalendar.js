/* CALENDAR */

function init_calendar() {

    if (typeof ($.fn.fullCalendar) === 'undefined') { return; }
    //console.log('init_calendar');

    myEvents = []

    //console.log("test12");
    myInviteEvents = []

   // console.log("test13");
    try {//to fix html encoding problem some utf-8 chars encoded when passing into js

       // console.log("test14");
        var elemEvents = document.createElement('textarea');
        elemEvents.innerHTML = eventList;
        var inputEvents = elemEvents.value;
        //hellojs = hellojs.replace(/&quot;/g, "\"");
        //console.log("test15");
        console.log(inputEvents);
        var obj = JSON.parse(inputEvents);
        //console.log("test16");
        for (var i = 0; i < obj.length; i++) {
            myEvents.push({ id: obj[i].EventId, title: obj[i].Name, start: obj[i].StartDate, end: obj[i].EndDate, description: obj[i].Description, location: obj[i].Location });
        }
        //console.log("test1");
        var elemInviteEvents = document.createElement('textarea');
        elemInviteEvents.innerHTML = inviteEventList;
        var inputInviteEvents = elemInviteEvents.value;
        //console.log("test2");
        var inviteObj = JSON.parse(inputInviteEvents);
        for (var i = 0; i < inviteObj.length; i++) {
            myInviteEvents.push({ id: inviteObj[i].EventId, holderEventId: inviteObj[i].HolderEventId, traineeId: inviteObj[i].UserId });
        }
        //console.log("test3");

    } catch (e) { }

    var date = new Date(),
        d = date.getDate(),
        m = date.getMonth(),
        y = date.getFullYear(),
        //for new event modal start
        //hour formatting is done
        hr = date.getHours(),
        endhr = hr + 1,
        min = date.getMinutes(),
        min = min < 10 ? '0' + min : min,
        ampm = hr >= 12 ? 'pm' : 'am',
        hr = hr == 12 ? 12 : hr % 12,
        hr = hr < 10 ? '0' + hr : hr,
        endampm = endhr >= 12 ? 'pm' : 'am',
        endhr = endhr == 12 ? 12 : endhr % 12,
        endhr = endhr < 10 ? '0' + endhr : endhr,
        //for new event modal end
        started,
        categoryClass;
    var options = {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    };

    var calendar = $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay,listMonth'
        },
        selectable: true,
        selectHelper: true,
        select: function (start, end, allDay) {
            //formatted date time object printed out into new event modal
            startDate = start._d.toLocaleDateString('tr-TR') + " " + hr + ":" + min + " " + ampm;
            endDate = start._d.toLocaleDateString('tr-TR', options) + " " + endhr + ":" + min + " " + endampm;

            $('#fc_create').click();
            console.log(start._d);


            //fill the datetime fields
            $('#startdate').val(startDate);
            $('#enddate').val(endDate);

            started = start;
            ended = end;

            $(".antosubmit").on("click", function () {
                var title = $("#title").val();
                if (end) {
                    ended = end;
                }

                categoryClass = $("#event_type").val();

                if (title) {
                    calendar.fullCalendar('renderEvent', {
                        title: title,
                        start: started,
                        end: end,
                        allDay: allDay
                    },
                        true // make the event "stick"
                    );
                }

                $('#title').val('');

                calendar.fullCalendar('unselect');

                $('.antoclose').click();

                return false;
            });
        },
        eventClick: function (calEvent, jsEvent, view) {
            //date formatting start
            startDate = calEvent.start._i;
            endDate = calEvent.end._i;
            startDate = startDate.replace("T", " ");
            endDate = endDate.replace("T", " ");

            //startDate
            sdate = startDate.substring(0, 10);
            sdateField = sdate.split("-");
            sdate = sdateField[2] + "." + sdateField[1] + "." + sdateField[0];

            //endDate
            edate = endDate.substring(0, 10);
            edateField = edate.split("-");
            edate = edateField[2] + "." + edateField[1] + "." + edateField[0];

            //startTime
            shour = startDate.substring(11, 13);
            sminute = startDate.substring(14, 16);

            //endTime
            ehour = endDate.substring(11, 13);
            eminute = endDate.substring(14, 16);

            sampm = shour >= 12 ? 'pm' : 'am',
                shour = shour == 12 ? 12 : shour % 12,
                shour = shour < 10 ? '0' + shour : shour

            eampm = ehour >= 12 ? 'pm' : 'am',
                ehour = ehour == 12 ? 12 : ehour % 12,
                ehour = ehour < 10 ? '0' + ehour : ehour

            sdate = sdate + " " + shour + ":" + sminute + " " + sampm;
            edate = edate + " " + ehour + ":" + eminute + " " + eampm;
            //date formatting end

            $('#fc_edit').click();
            $('#eventId2').val(calEvent.id);
            $('#title2').val(calEvent.title);
            $('#description2').val(calEvent.description);
            $('#location2').val(calEvent.location);
            $('#startdate2').val(sdate);
            $('#enddate2').val(edate);

            //console.log(calEvent.id);
            for (var i = 0; i < myInviteEvents.length; i++) {
                document.getElementById('CheckId' + myInviteEvents[i].traineeId).checked = false;
            }
            for (var i = 0; i < myInviteEvents.length; i++) {
                //console.log(document.getElementById('CheckId' + myInviteEvents[i].traineeId).checked);
                if (myInviteEvents[i].holderEventId == calEvent.id) {
                    document.getElementById('CheckId' + myInviteEvents[i].traineeId).checked = true;
                }
            }

            categoryClass = $("#event_type").val();

            $(".antosubmit2").on("click", function () {
                calEvent.title = $("#title2").val();

                calendar.fullCalendar('updateEvent', calEvent);
                $('.antoclose2').click();
            });

            calendar.fullCalendar('unselect');
        },
        editable: true,
        eventRender: function (event, element, view) {
            //element.find('.fc-event-skin').addClass(event.className.join(' '));
            //return $('<div>' + event.title + '</div>');
        },
        events: myEvents,
        //eventColor: '#68a3d6'
    });

};

$(document).ready(function() {	
	init_calendar();			
});	