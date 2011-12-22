var xBoard = 0;
var oBoard = 0;
var begin = true;
var context;
var width, height;

var boardSize=20;
var nmbRows = boardSize;
var nmbColumns = boardSize;

var userSq= 1;
var machSq=-1;

var machTurn=false;
var winningMove=9999999;
var openFour   =8888888;
var twoThrees  =7777777;

f=new Array();
s=new Array();
q=new Array();

iMax=new Array();
jMax=new Array();
nMax=0;

for (i=0;i<boardSize;i++) {
    f[i]=new Array();
    s[i]=new Array();
    q[i]=new Array();
    for (j=0;j<boardSize;j++) {
        f[i][j]=0;
        s[i][j]=0;
        q[i][j]=0;
    }
}

iLastUserMove=0;
jLastUserMove=0;

var drawPos=0;
w=new Array(0,20,17,15.4,14,10);
nPos=new Array();
dirA=new Array();

function paintBoard() {
    var board = document.getElementById('board');
  
    width = board.width;
    height = board.height;
    context = board.getContext('2d');

    context.beginPath();
    context.strokeStyle = '#000'; 
    context.lineWidth   = 2;

    var i=0; 
    for (i=0;i<=nmbRows+1;i++)
    {
       context.moveTo(0, (height / nmbRows) * i);
       context.lineTo(width, (height / nmbRows) * i);
    }
    var j =0;
    for (j=0;j<=nmbColumns+1;j++)
    {
        context.moveTo((width / nmbColumns) * j, 0);
        context.lineTo((width / nmbColumns) * j, height);
    }

    context.stroke();
    context.closePath(); 
}

function resetGame() {
    machTurn=0;
    gameOver=0;
    drawPos=0;

    for (i=0;i<boardSize;i++) {
        for (j=0;j<boardSize;j++) {
            f[i][j]=0;
        }
    }
    
    var board = document.getElementById('board');
    context = board.getContext('2d');
    context.clearRect(0,0,width,height);
   
    paintBoard();
}

function clk(iMove,jMove) {
    if (machTurn) return; //machine (computer) turn

    if (f[iMove][jMove]!=0) {alert('This square is not empty! Please choose another.'); return; }
    f[iMove][jMove]=userSq;
    drawSquare(iMove,jMove,userSq);
    machTurn=true;
    iLastUserMove=iMove;
    jLastUserMove=jMove;
    
    if (winningPos(iMove,jMove,userSq)==winningMove) {
        gameOver=1;
        alert('You won!');
    }
    else
        machineMove(iLastUserMove,jLastUserMove);
}

function machineMove(iUser,jUser) {
    getBestMachMove();
    f[iMach][jMach]=machSq;
    drawSquare(iMach,jMach,machSq);

    if (winningPos(iMach,jMach,machSq)==winningMove) {
        gameOver=1;
        alert("Machine won!");
    }
    else if (drawPos) {
        gameOver=1;
        alert("It\'s a draw!");
    }
    else 
        machTurn=false;
}

function getBestMachMove() {
 maxS=evaluatePos(s,userSq);
 maxQ=evaluatePos(q,machSq);

 // alert ('maxS='+maxS+', maxQ='+maxQ);

 if (maxQ>=maxS) {
  maxS=-1;
  for (i=0;i<boardSize;i++) {
   for (j=0;j<boardSize;j++) {
    if (q[i][j]==maxQ) {
     if (s[i][j]>maxS) {maxS=s[i][j]; nMax=0}
     if (s[i][j]==maxS) {iMax[nMax]=i;jMax[nMax]=j;nMax++} 
    }
   }
  }
 }
 else {
  maxQ=-1;
  for (i=0;i<boardSize;i++) {
   for (j=0;j<boardSize;j++) {
    if (s[i][j]==maxS) {
     if (q[i][j]>maxQ) {maxQ=q[i][j]; nMax=0}
     if (q[i][j]==maxQ) {iMax[nMax]=i;jMax[nMax]=j;nMax++} 
    }
   }
  }
 }
 // alert('nMax='+nMax+'\niMax: '+iMax+'\njMax: '+jMax)

 randomK=Math.floor(nMax*Math.random());
 iMach=iMax[randomK];
 jMach=jMax[randomK];
}

function evaluatePos(a,mySq) {
 maxA=-1;
 drawPos=true;

 for (i=0;i<boardSize;i++) {
  for (j=0;j<boardSize;j++) {

   // Compute "value" a[i][j] of the (i,j) move

   if (f[i][j]!=0) {a[i][j]=-1; continue;}  
   if (hasNeighbors(i,j)==0) {a[i][j]=-1; continue;}

   wp=winningPos(i,j,mySq);
   if (wp>0) a[i][j]=wp;
   else {
    minM=i-4; if (minM<0) minM=0;
    minN=j-4; if (minN<0) minN=0;
    maxM=i+5; if (maxM>boardSize) maxM=boardSize;
    maxN=j+5; if (maxN>boardSize) maxN=boardSize;

    nPos[1]=1; A1=0;
    m=1; while (j+m<maxN  && f[i][j+m]!=-mySq) {nPos[1]++; A1+=w[m]*f[i][j+m]; m++}
    if (j+m>=boardSize || f[i][j+m]==-mySq) A1-=(f[i][j+m-1]==mySq)?(w[5]*mySq):0;
    m=1; while (j-m>=minN && f[i][j-m]!=-mySq) {nPos[1]++; A1+=w[m]*f[i][j-m]; m++}   
    if (j-m<0 || f[i][j-m]==-mySq) A1-=(f[i][j-m+1]==mySq)?(w[5]*mySq):0;
    if (nPos[1]>4) drawPos=false;

    nPos[2]=1; A2=0;
    m=1; while (i+m<maxM  && f[i+m][j]!=-mySq) {nPos[2]++; A2+=w[m]*f[i+m][j]; m++}
    if (i+m>=boardSize || f[i+m][j]==-mySq) A2-=(f[i+m-1][j]==mySq)?(w[5]*mySq):0;
    m=1; while (i-m>=minM && f[i-m][j]!=-mySq) {nPos[2]++; A2+=w[m]*f[i-m][j]; m++}   
    if (i-m<0 || f[i-m][j]==-mySq) A2-=(f[i-m+1][j]==mySq)?(w[5]*mySq):0; 
    if (nPos[2]>4) drawPos=false;

    nPos[3]=1; A3=0;
    m=1; while (i+m<maxM  && j+m<maxN  && f[i+m][j+m]!=-mySq) {nPos[3]++; A3+=w[m]*f[i+m][j+m]; m++}
    if (i+m>=boardSize || j+m>=boardSize || f[i+m][j+m]==-mySq) A3-=(f[i+m-1][j+m-1]==mySq)?(w[5]*mySq):0;
    m=1; while (i-m>=minM && j-m>=minN && f[i-m][j-m]!=-mySq) {nPos[3]++; A3+=w[m]*f[i-m][j-m]; m++}   
    if (i-m<0 || j-m<0 || f[i-m][j-m]==-mySq) A3-=(f[i-m+1][j-m+1]==mySq)?(w[5]*mySq):0; 
    if (nPos[3]>4) drawPos=false;

    nPos[4]=1; A4=0;
    m=1; while (i+m<maxM  && j-m>=minN && f[i+m][j-m]!=-mySq) {nPos[4]++; A4+=w[m]*f[i+m][j-m]; m++;}
    if (i+m>=boardSize || j-m<0 || f[i+m][j-m]==-mySq) A4-=(f[i+m-1][j-m+1]==mySq)?(w[5]*mySq):0;
    m=1; while (i-m>=minM && j+m<maxN  && f[i-m][j+m]!=-mySq) {nPos[4]++; A4+=w[m]*f[i-m][j+m]; m++;} 
    if (i-m<0 || j+m>=boardSize || f[i-m][j+m]==-mySq) A4-=(f[i-m+1][j+m-1]==mySq)?(w[5]*mySq):0;
    if (nPos[4]>4) drawPos=false;

    dirA[1] = (nPos[1]>4) ? A1*A1 : 0;
    dirA[2] = (nPos[2]>4) ? A2*A2 : 0;
    dirA[3] = (nPos[3]>4) ? A3*A3 : 0;
    dirA[4] = (nPos[4]>4) ? A4*A4 : 0;

    A1=0; A2=0;
    for (k=1;k<5;k++) {
     if (dirA[k]>=A1) {A2=A1; A1=dirA[k]}
    }
    a[i][j]=A1+A2;
   }
   if (a[i][j]>maxA) {
    maxA=a[i][j];
   }
  }
 }
 return maxA;
}

function hasNeighbors(i,j) {
 if (j>0 && f[i][j-1]!=0) return 1;
 if (j+1<boardSize && f[i][j+1]!=0) return 1; 
 if (i>0) {
  if (f[i-1][j]!=0) return 1;
  if (j>0 && f[i-1][j-1]!=0) return 1;
  if (j+1<boardSize && f[i-1][j+1]!=0) return 1;
 }
 if (i+1<boardSize) {
  if (f[i+1][j]!=0) return 1;
  if (j>0 && f[i+1][j-1]!=0) return 1;
  if (j+1<boardSize && f[i+1][j+1]!=0) return 1;
 }
 return 0;
}

function winningPos(i,j,mySq) {
 test3=0;
 test4=0;

 L=1;
 m=1; while (j+m<boardSize  && f[i][j+m]==mySq) {L++; m++} m1=m;
 m=1; while (j-m>=0 && f[i][j-m]==mySq) {L++; m++} m2=m;   
 if (L>4) { return winningMove; }
 side1=(j+m1<boardSize && f[i][j+m1]==0);
 side2=(j-m2>=0 && f[i][j-m2]==0);

 if (L==4 && (side1 || side2)) test3++;
 if (side1 && side2) {
  if (L==4) test4=1;
  if (L==3) test3++;
 }

 L=1;
 m=1; while (i+m<boardSize  && f[i+m][j]==mySq) {L++; m++} m1=m;
 m=1; while (i-m>=0 && f[i-m][j]==mySq) {L++; m++} m2=m;   
 if (L>4) { return winningMove; }
 side1=(i+m1<boardSize && f[i+m1][j]==0);
 side2=(i-m2>=0 && f[i-m2][j]==0);
 if (L==4 && (side1 || side2)) test3++;
 if (side1 && side2) {
  if (L==4) test4=1;
  if (L==3) test3++;
 }

 L=1;
 m=1; while (i+m<boardSize && j+m<boardSize && f[i+m][j+m]==mySq) {L++; m++} m1=m;
 m=1; while (i-m>=0 && j-m>=0 && f[i-m][j-m]==mySq) {L++; m++} m2=m;   
 if (L>4) { return winningMove; }
 side1=(i+m1<boardSize && j+m1<boardSize && f[i+m1][j+m1]==0);
 side2=(i-m2>=0 && j-m2>=0 && f[i-m2][j-m2]==0);
 if (L==4 && (side1 || side2)) test3++;
 if (side1 && side2) {
  if (L==4) test4=1;
  if (L==3) test3++;
 }

 L=1;
 m=1; while (i+m<boardSize  && j-m>=0 && f[i+m][j-m]==mySq) {L++; m++} m1=m;
 m=1; while (i-m>=0 && j+m<boardSize && f[i-m][j+m]==mySq) {L++; m++} m2=m; 
 if (L>4) { return winningMove; }
 side1=(i+m1<boardSize && j-m1>=0 && f[i+m1][j-m1]==0);
 side2=(i-m2>=0 && j+m2<boardSize && f[i-m2][j+m2]==0);
 if (L==4 && (side1 || side2)) test3++;
 if (side1 && side2) {
  if (L==4) test4=1;
  if (L==3) test3++;
 }

 if (test4) return openFour;
 if (test3>=2) return twoThrees;
 return -1;
}

function drawSquare(x,y,who) {
    if (who==1) {//user 
        paintO(x,y);
    }
    else if (who==-1){//machine
        paintX(x,y);
    }
}

function paintX(x, y) {

   context.beginPath();

   context.strokeStyle = '#ff0000'; 
   context.lineWidth   = 2;

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
   context.lineWidth   = 2;

   var offsetX = (width / nmbColumns) * 0.1;
   var offsetY = (height / nmbRows) * 0.1;

   var beginX = x * (width / nmbColumns) + offsetX;
   var beginY = y * (height / nmbRows) + offsetY;

   var endX = (x + 1) * (width / nmbColumns) - offsetX * 2;
   var endY = (y + 1) * (height / nmbRows) - offsetY * 2;

   context.arc(beginX + ((endX - beginX) / 2), beginY + ((endY - beginY) / 2), (endX - beginX) / 2 , 0, Math.PI * 2, true);

   context.stroke();
   context.closePath();
}

function clickHandler(e) {
    var y = Math.floor((e.clientY-8) / (height / nmbRows));    
    var x =  Math.floor((e.clientX-8) / (width/ nmbColumns)); 

    clk(x,y);
}