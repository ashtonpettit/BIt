<template>

    <div id="sprite-container">
              <div class="sadSprite">
                <div class="ear-left">
                    <div class="inner-ear"></div>
                </div>

                <div class="ear-right">
                    <div class="inner-ear"></div>
                </div>
                  <div class="sadleg-left"></div>
                  <div class="sadleg-right"></div>
                  <div class="eye1"></div>
                  <div class="eye2"></div>
                  <div class="nose"></div>
                  <div class="mouth"></div>
                  <div class="red-ball"></div>
                  <div class="sadshadow"></div>
  
              </div>
      
          </div>

</template>

<script>
export default {
    name: 'DataSprite'
  }
  
  </script>
  
  <style scoped>
  #sprite-container{
      position: relative;
      float: left;
      height: 375px;
      width: 350px;
      transform: translate(-50%,-50%);
      top: 35%;
      left: 15%;
  }
  
  .sadSprite{
      background-color: #A6BECF;
      height: 180px;
      width: 140px;
      border-radius: 65px;
      position: relative;
      top: 100px;
      left: 0;
      right: 0;
      margin: auto;
      background-size: 200% 200%;
      background-position: 0 17px;
      animation: jump 0.5s infinite;
  }
  
  .sadSprite:before,
  .sadSprite:after{
  
      position: absolute;
      content: "";
      height: 80px;
      width: 32px;
      background-color: #6c7d8a;
      top: 25px;
      z-index: -1;
      transform-origin: bottom;
      border-radius: 25px;
  
  }
  
  .sadSprite:before{
      left: 10px;
      transform: rotate(-50deg);
      animation: hand-l 0.5s infinite;
  }
  @keyframes hand-l{
      50%{
          transform: rotate(-42deg);
      }
  }
  .sadSprite:after{
      right: 10px;
      transform: rotate(50deg);
      animation: hand-r 0.5s infinite;
  }
  @keyframes hand-r{
      50%{
          transform: rotate(42deg);
      }
  }
  .ear-left {
  position: absolute;
  width: 60%;
  height: 65%;
  left: -20%;
  top: -25%;
  background: #A6BECF;
  border-radius: 50%;
  z-index: -2;
}

.ear-right {
  position: absolute;
  width: 60%;
  height: 65%;
  right: -20%;
  top: -25%;
  background: #A6BECF;
  border-radius: 50%;
  z-index: -2;
}

.inner-ear{
  position: absolute;
  border-radius: 50%;
  width: 80%;
  height: 80%;
  top: 10%;
  left: 10%;
  background: #819CAF;
}
  .eye1{
  width: 30px;
  height: 30px;
  border-radius: 100%;
  background-color: #000;
  position: absolute;
  left: 25px;
  top: 40px;
}
.eye2{
  width: 30px;
  height: 30px;
  border-radius: 100%;
  background-color: #000;
  position: absolute;
  right: 25px;
  top: 40px;
}
.eye1:before{
  content: "";
  position: absolute;
  width: 8px;
  height: 8px;
  background-color: #fff;border-radius: 100%;
  top: 7px;
  right: 8px;
}

.eye2:before{
  content: "";
  position: absolute;
  width: 8px;
  height: 8px;
  background-color: #fff;border-radius: 100%;
  top: 7px;
  right: 8px;
}
.nose{
  width: 0px;
  height: 0px;
  border-left: 10px solid transparent;
  border-right: 10px solid transparent;
  border-top: 10px solid black;
  position: absolute;
  top: 70px;
  left: 60px;

}

.mouth{
  width: 10px;
  height: 10px;
  position: absolute;
  left: 70px;
  top: 80px;
}
.mouth:before{
  width: 30px;
  height: 20px;
  border: 4px solid black;
  content: "";
  position: absolute;
  border-bottom-left-radius: 100%;
  border-bottom-right-radius: 100%;
  border-top: 0;
  border-left: 0;
  border-right: 0;
}


.mouth:after{
  width: 30px;
  height: 20px;
  border: 4px solid black;
  content: "";
  position: absolute;
  border-bottom-left-radius: 100%;
  border-bottom-right-radius: 100%;
  border-top: 0;
  border-left: 0;
  border-right: 0;
  left: -30px;
}

.red-ball{
  width: 15px;
  height: 15px;
  background-color: #ef1c2d;
  border-radius: 100%;
  position: absolute;
  left: 20px;
  top: 75px;
  box-shadow: 85px 0px #ef1c2d;
}
  .sadleg-left,
  .sadleg-right{
      height: 80px;
      width: 32px;
      background-color: #6c7d8a;
      position: absolute;
      top: 130px;
      border-radius: 25px;
      z-index: -1;
  }
  .sadleg-left{
      left: 22px;
      animation: legs 0.6s 0.3s infinite;
  }
  .sadleg-right{
      right: 22px;
      animation: legs 0.6s infinite;
  }
  
  @keyframes legs{
      50%{
          transform: translateY(-15px);
      }
  }
  
  @keyframes jump{
      50%{
          top: 95px;
      }
  }
  
  .sadshadow{
      height: 30px;
      width: 160px;
      background-color: rgba(30,30,30,0.05);
      border-radius: 50%;
      position: absolute;
      top: 290px;
      z-index: -2;
      left: 0;
      right: 0;
      animation: shadow 0.5s infinite;
  }
  @keyframes shadow{
      50%{
          transform: scaleX(0.85);
      }
  }
  
  
  </style>