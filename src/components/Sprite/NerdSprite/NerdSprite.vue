<template>

  <div id="sprite-container">
            <div class="blackSprite">
              <div class="bolt"></div>
                <div class="blackleg-left"></div>
                <div class="blackleg-right"></div>

                <div class="eyes blackeye-left">
                  <div class="eye-ball"></div>
                  <div class="line"></div>
                </div>
                <div class="eyes blackeye-right">
                  <div class="eye-ball-2"></div>
                </div>

                <div class="blackmouth"></div>
                <div class="blackshadow"></div>


            </div>
    
        </div>

</template>

<script>

export default {
name: 'BlackSprite'
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

.bolt {
  position: absolute;
  left: 60px;
  top: 10px;
  border: 4px solid rgba(0,0,0,0);
  border-bottom: 22px solid #ffdf00;
  transform: rotateZ(-160deg);
  width: 0;
  height: 0;
  margin: 0;
  overflow: visible;
  border-top: 0 solid;
  border-radius: 0;
  transform-origin: 50% 50% 0;
}

.bolt:after {
content: "";
top: -9px;
left: -8px;
border: 5px solid rgba(0,0,0,0);
border-bottom: 25px solid #ffdf00;
transform: rotateZ(4deg);
padding: 0;
width: 0;
height: 0;
position: absolute;
margin: 0;
overflow: visible;
border-top: 0 solid;
border-radius: 0;
transform-origin: 50% 50% 0;
}

.eyes{
      width: 30px;
      height: 30px;
      border-radius: 50%;
      background: whitesmoke;
}
.blackeye-left{
      transform: translate(25px, 50px)
}

.blackeye-left .eye-ball,
.blackeye-right .eye-ball-2{
      width: 70%;
      height: 70%;
      position: absolute;
      border-radius: 50%;
      background: #BBCCDA;
      position: relative;
      display: flex;
      justify-content: center;
      align-items: center;
}
/*
.blackeye-left .eye-ball{
      transform: translate(21px, 18px)
} */
.eyes::after{
      content: '';
      position: absolute;
      width: 148%;
      height: 148%;
      background: transparent;
      border: 4px solid black;
      border-radius: 50%;
}
.blackeye-right::after{
      transform: translate(-10px, -30px)
}
.blackeye-left::after{
      transform: translate(-10px, -30px)
}
.eye-ball::before,
.eye-ball-2::before{
      content: '';
      position: absolute;
      width: 80%;
      height: 80%;
      border-radius: 50%;
      background: black;
} 

.blackeye-right{
      transform: translate(80px, 20px)
}.line{
      width: 5px;
      height: 4px;
      background: #000;
      position: absolute;
      left: 40px;
      bottom: 10px;
      z-index: -1;
}

.blackSprite{
  background-color: #806b73;
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
  border-radius: 50% 50% 50% 50%/ 60% 60% 40% 40%;
}

.blackSprite:before,
.blackSprite:after{

  position: absolute;
  content: "";
  height: 80px;
  width: 32px;
  background-color: #bbb3a2;
  top: 25px;
  z-index: -1;
  transform-origin: bottom;
  border-radius: 25px;

}

.blackSprite:before{
  left: 10px;
  transform: rotate(-50deg);
  animation: hand-l 0.5s infinite;
}
@keyframes hand-l{
  50%{
      transform: rotate(-42deg);
  }
}
.blackSprite:after{
  right: 10px;
  transform: rotate(50deg);
  animation: hand-r 0.5s infinite;
}
@keyframes hand-r{
  50%{
      transform: rotate(42deg);
  }
}

.blackleaf{
  background-color: #498725;
  height: 70px;
  width: 70px;
  border-radius: 70px 0;
  transform: rotate(135deg);
  position: absolute;
  top: -65px;
  z-index: -1;
  margin: auto;
  left: 0;
  right: 0;
  animation: leaves 0.5s infinite;
}
@keyframes leaves{
  50%{
      transform: rotate(135deg) translate(6px, -6px);
  }
}
.blackleaf:before,
.blackleaf:after{
  position: absolute;
  content: "";
  background-color: #3f6b15;
  height: 70px;
  width: 70px;
  border-radius: 70px 0;
  transform-origin: right;
  top: -5px;

}

.blackleaf:before{
  transform: rotate(40deg);
  right: 20px;
  animation: leaf-right 0.5s infinite;
}
@keyframes leaf-right{
  50%{
      transform: rotate(32deg);
  }
}
.blackleaf:after{
  transform: rotate(-40deg);
  left: 20px;
  animation: leaf-left 0.5s infinite;
}
@keyframes leaf-left{
  50%{
      transform: rotate(-32deg);
  }
}

.blackleg-left,
.blackleg-right{
  height: 80px;
  width: 32px;
  background-color: #bbb3a2;
  position: absolute;
  top: 130px;
  border-radius: 25px;
  z-index: -1;
}
.blackleg-left{
  left: 22px;
  animation: legs 0.6s 0.3s infinite;
}
.blackleg-right{
  right: 22px;
  animation: legs 0.6s infinite;
}

@keyframes legs{
  50%{
      transform: translateY(-15px);
  }
}

.blackmouth{
  height: 20px;
  width: 25px;
  background-color: black;
  border-radius: 0 0 35px 35px;
  position: absolute;
  margin: auto;
  left: 0;
  right: 0;
  top: 105px;

}

@keyframes jump{
  50%{
      top: 95px;
  }
}

.blackshadow{
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