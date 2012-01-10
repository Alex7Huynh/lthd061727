var context;
var myTurn;
var gameOver;

var userSq = 1;
var oppSq = -1;

f = new Array();

var width, height;
var boardSize = 20;
var nmbRows = boardSize;
var nmbColumns = boardSize;

for (i = 0; i < boardSize; i++) {
    f[i] = new Array();
    for (j = 0; j < boardSize; j++) {
        f[i][j] = 0;
    }
};

var intervalId;

function paintBoard() {
    var board = document.getElementById('board');

    width = board.width;
    height = board.height;
    context = board.getContext('2d');

    context.beginPath();
    context.strokeStyle = '#000';
    context.lineWidth = 2;

    var i = 0;
    for (i = 0; i <= nmbRows + 1; i++) {
        context.moveTo(0, (height / nmbRows) * i);
        context.lineTo(width, (height / nmbRows) * i);
    }
    var j = 0;
    for (j = 0; j <= nmbColumns + 1; j++) {
        context.moveTo((width / nmbColumns) * j, 0);
        context.lineTo((width / nmbColumns) * j, height);
    }

    context.stroke();
    context.closePath();
}

function initGame() {
    myTurn = false;
    gameOver = false;

    for (i = 0; i < boardSize; i++) {
        for (j = 0; j < boardSize; j++) {
            f[i][j] = 0;
        }
    }

    var board = document.getElementById('board');
    context = board.getContext('2d');
    context.clearRect(0, 0, width, height);

    paintBoard();
    waitingForOpponent();
}

function waitingForOpponent() {
    intervalId = setInterval("tick()", 2000);
}

function tick() {
    if (!myTurn) {
        myTurn = IsMyTurn();
        if (myTurn == true) {
            var lastMove = GetOpponentMove();
            alert('last Move' + lastMove);
            alert('your turn');
            opponentMove(lastMove);
        }
    }
}

function opponentMove(move) {
    if (f[move[0]][move[1]] == 0) {
        f[move[0]][move[1]] = oppSq;
        drawSquare(move[0], move[1], oppSq);

        checkGameOver();
    }
}

function checkGameOver() {
    gameOver = IsGameOver();
    if (gameOver) {
        alert('over');

        var win = IsWin();
        if (win)
            alert('You win!');
        else
            alert('You loose!');

        clearInterval(intervalId);
    }
}

function drawSquare(x, y, who) {
    if (who == 1) {//user 
        paintO(x, y);
    }
    else if (who == -1) {//machine
        paintX(x, y);
    }
}

function paintX(x, y) {

    context.beginPath();

    context.strokeStyle = '#ff0000';
    context.lineWidth = 2;

    var offsetX = (width / nmbColumns) * 0.1;
    var offsetY = (height / nmbRows) * 0.1;

    var beginX = x * (width / nmbColumns) + offsetX;
    var beginY = y * (height / nmbRows) + offsetY;

    var endX = (x + 1) * (width / nmbColumns) - offsetX * 2;
    var endY = (y + 1) * (height / nmbRows) - offsetY * 2;

    context.moveTo(beginX, beginY);
    context.lineTo(endX, endY);

    context.moveTo(beginX, endY);
    context.lineTo(endX, beginY);

    context.stroke();
    context.closePath();
}

function paintO(x, y) {

    context.beginPath();

    context.strokeStyle = '#0000ff';
    context.lineWidth = 2;

    var offsetX = (width / nmbColumns) * 0.1;
    var offsetY = (height / nmbRows) * 0.1;

    var beginX = x * (width / nmbColumns) + offsetX;
    var beginY = y * (height / nmbRows) + offsetY;

    var endX = (x + 1) * (width / nmbColumns) - offsetX * 2;
    var endY = (y + 1) * (height / nmbRows) - offsetY * 2;

    context.arc(beginX + ((endX - beginX) / 2), beginY + ((endY - beginY) / 2), (endX - beginX) / 2, 0, Math.PI * 2, true);

    context.stroke();
    context.closePath();
}

function clickHandler(e) {
    // jQuery would normalize the event
    position = getPosition(e);
    //now you can use the x and y positions
    //alert("X: " + position.x + " Y: " + position.y);

    var y = Math.floor((position.y) / (height / nmbRows));
    var x = Math.floor((position.x) / (width / nmbColumns));

    clk(x, y);
}

function getPosition(e) {

    //this section is from http://www.quirksmode.org/js/events_properties.html
    var targ;
    if (!e)
        e = window.event;
    if (e.target)
        targ = e.target;
    else if (e.srcElement)
        targ = e.srcElement;
    if (targ.nodeType == 3) // defeat Safari bug
        targ = targ.parentNode;

    // jQuery normalizes the pageX and pageY
    // pageX,Y are the mouse positions relative to the document
    // offset() returns the position of the element relative to the document
    var x = e.pageX - $(targ).offset().left;
    var y = e.pageY - $(targ).offset().top;

    return { "x": x, "y": y };
};

function clk(iMove, jMove) {
    if (!gameOver && myTurn) {
        myTurn = false;
        if (f[iMove][jMove] == 0) {
            f[iMove][jMove] = userSq;
            drawSquare(iMove, jMove, userSq);
            UserMove(iMove, jMove);
        }
        checkGameOver();
    }
}