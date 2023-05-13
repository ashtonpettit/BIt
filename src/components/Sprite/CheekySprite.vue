<template>

    <div id="sprite-container">
              <div class="pineapple">
                    <div class="ear ear--left"></div>
                    <div class="ear ear--right"></div>
                    <div class="head">
                    <div class="eye eye--left">
                        <div class="eye-inside-left"></div>
                    </div>
                    <div class="nose"></div>
                    <div class="eye eye--right">
                        <div class="eye-inside-right"></div>
                    </div>
                    <div class="mouth">
                        <div class="mouth-left"></div>
                        <div class="mouth-right"></div>
                    </div>
                    </div>
                    <div class="leg-left"></div>
                    <div class="leg-right"></div>
                    <div class="shadow"></div>
  
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
.head{
    position: absolute;
    background-color: white;
    width: 180px;
    height: 140px;
    top: 10px;
    left: 70px;
    transform: translate(-50%, -50%);
    border-radius: 50% 50% 40% 40%;
    box-shadow: inset 5vh 1vh 0 1vh rgb(65, 63, 63, 0.2);
    z-index: 1;
}
.eye{
    position: absolute;
    width: 50px;
    height: 55px;
    background-color: black;
    border-radius: 100%;
    z-index: 2;
}
.eye--left{
    top: 30px;
    left: 30px;
    
}
.eye-inside-left{
    position: absolute;
    width: 15px;
    height: 20px;
    left: 10px;
    top: 20px;
    box-shadow: inset 1vh 1vh 0 2vh #fff;
    border-radius: 100%;
}
.eye--right{
    top: 30px;
    left: 100px;
}
.eye-inside-right{
    position: absolute;
    width: 15px;
    height: 20px;
    left: 10px;
    top: 20px;
    box-shadow: inset -1vh 1vh 0 2vh #fff;
    border-radius: 100%;
}
.nose {
      position: absolute;
      width: 10px;
      height: 12px;
      left: 85px;
      top: 85px;
      background-color: black;
      border-radius: 3vh 3vh 100% 100%;
      z-index: 3;
    }

.mouth{
    position: absolute;
    width: 25px;
    height: 20px;
    top: 90px;
    left: 85px;
    display: flex;
    justify-content: center;
    align-items: flex-start;
}
.mouth-left{
    width: 25px;
    height: 10px;
    border-radius: 50%;
}
.pineapple{
    background-color: #fff;
    height: 180px;
    width: 140px;
    border-radius: 65px;
    position: relative;
    top: 100px;
    left: 0;
    right: 0;
    margin: auto;
    animation: jump 0.5s infinite;
}

.pineapple:before,
.pineapple:after{

    position: absolute;
    content: "";
    height: 80px;
    width: 32px;
    background-color: #fff;
    top: 25px;
    z-index: -1;
    transform-origin: bottom;
    border-radius: 25px;

}

.pineapple:before{
    left: 10px;
    transform: rotate(-50deg);
    animation: hand-l 0.5s infinite;
}
@keyframes hand-l{
    50%{
        transform: rotate(-42deg);
    }
}
.pineapple:after{
    right: 10px;
    transform: rotate(50deg);
    animation: hand-r 0.5s infinite;
}
@keyframes hand-r{
    50%{
        transform: rotate(42deg);
    }
}

.leg-left,
.leg-right{
    height: 80px;
    width: 32px;
    background-color: #fff;
    position: absolute;
    top: 130px;
    border-radius: 25px;
    z-index: -1;
}
.leg-left{
    left: 22px;
    animation: legs 0.6s 0.3s infinite;
}
.leg-right{
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

.shadow{
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