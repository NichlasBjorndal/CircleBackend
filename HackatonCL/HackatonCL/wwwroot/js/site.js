// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var canvas = document.getElementById('paint');
var ctx = canvas.getContext('2d');

var sketch = document.getElementById('sketch');
var sketch_style = getComputedStyle(sketch);
canvas.width = 500;
canvas.height = 250;

var mouse = { x: 0, y: 0 };

document.getElementById("btnclear").addEventListener("click", clear);

/* Mouse Capturing Work */
canvas.addEventListener('mousemove', function (e) {
    mouse.x = e.pageX - this.offsetLeft;
    mouse.y = e.pageY - this.offsetTop;
}, false);

/* Drawing on Paint App */
ctx.lineJoin = 'round';
ctx.lineCap = 'round';

ctx.strokeStyle = "red";
function getColor(colour) { ctx.strokeStyle = colour; }

function getSize(size) { ctx.lineWidth = size; }


//ctx.strokeStyle = 
//ctx.strokeStyle = document.settings.colour[1].value;

canvas.addEventListener('mousedown', function (e) {
    ctx.beginPath();
    ctx.moveTo(mouse.x, mouse.y);

    canvas.addEventListener('mousemove', onPaint, false);
}, false);

canvas.addEventListener('mouseup', function () {
    canvas.removeEventListener('mousemove', onPaint, false);
}, false);

var onPaint = function () {
    ctx.lineTo(mouse.x, mouse.y);
    ctx.stroke();
};


function saveImage() {
    var image = document.getElementById("paint").toDataURL("image/png");
    image = image.replace('data:image/png;base64,', '');
    var ajax = new XMLHttpRequest();
    ajax.open("POST", 'https://localhost:44302/api/pictureUpload', false);
    ajax.setRequestHeader('Content-Type', 'application/upload');
    ajax.send(image);
}

function clear() {
    var context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);
}

function sendImage() {
	var xmlhttp = new XMLHttpRequest();

	xmlhttp.onreadystatechange = function () {
		if (xmlhttp.readyState == XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
            if (xmlhttp.status == 200) {
                document.getElementById("picturetosend").value = xmlhttp.response;

                var roomname = document.getElementById("group").value;
                connection.invoke("updateTask", roomname).catch(function (err) {
                    return console.error(err.toString());
                });

                event.preventDefault();
			}
			else if (xmlhttp.status == 400) {
				console.log('There was an error 400');
			}
			else {
				console.log('something else other than 200 was returned');
			}
		}
	};

    canvas.toBlob(function (blob) {

	    var formData = new FormData();

	    formData.append('file', blob);
	    xmlhttp.open('POST', 'https://localhost:44302/api/pictureUpload');
	    xmlhttp.send(formData);
    }, "image/png" );

    

    
}

//canvasEl.toBlob(blob =>
//	this.drawingService.uploadFile(blob).subscribe((data: Response) => console.log(data.statusText)), "image/png");