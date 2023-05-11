<template>

    <div id="sprite-container">
              <div class="pineapple">
                  <div class="leg-left"></div>
                  <div class="leg-right"></div>
                  <div class = 'head'>
                <div class = 'eye-left'>
                    <div class = 'inner-eye'></div>
                </div>
                <div class = 'eye-right'>
                    <div class = 'eye-right-part one'></div>
                    <div class = 'eye-right-part two'></div>
                </div>
                <div class = 'nose'></div>
                <div class = 'nose-inner'></div>
                <div class = 'whisker whisker-left'></div>
                <div class = 'whisker-part whisker-left-one'></div>
                <div class = 'whisker whisker-right'></div>
                <div class = 'whisker-part whisker-right-one'></div>
                <div class = 'whisker-part whisker-top'></div>
                <div class = 'mouth'>
                    <div class = 'tongue-part-one'>
                        <div class = 'tongue-inner-part-one'></div>
                        <div class = 'tongue-inner-part-two'></div>
                    </div>
                    <div class = 'tongue-part-two'></div>
                </div>
            </div>
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

.nose {
    width: 45px;
    height: 55px;
    position: absolute;
    background-color: #364F60;
    border-radius: 62%/75%;
    top: 55px;
    left: 50px;
}

.nose-inner {
    width: 15px;
    height: 25px;
    background-color: #6C8596;
    position: absolute;
    border-radius: 50%;
    transform: rotate(-30deg);
    top: 60px;
    left: 70px;
}
.eye-left {
    width: 34px;
    height: 34px;
    background-color: #364F60;
    border-radius: 50%;
    position: absolute;
    top: 55px;
    left: 10px;
}
.inner-eye {
    position: absolute;
    width: 13px;
    height: 13px;
    background-color: #fff;
    border-radius: 50%;
    left: 15px;
    top: 5px;
}
.whisker {
    position: absolute;
    width: 42px;
    height: 35px;
    border: solid 8px #364F60;
    border-color: transparent transparent #364F60 transparent;
    border-radius: 50%/25px 25px;
    top: 40px;
    left: 100px;
}
/*
.whisker-left {
    top: 156px;
    left: 106px;
}

.whisker-right {
    top: 156px;
    left: 143px;
}

.whisker-part {
    position: absolute;
    width: 7.8px;
    height: 20px;
    background-color: var(--nose-eye-color);
    border-radius: 66%/30%;
}

.whisker-left-one {
    top: 185px;
    left: 111px;
    transform: rotate(-52deg);
}

.whisker-right-one {
    top: 185px;
    left: 188px;
    transform: rotate(54deg);
}

.whisker-top {
    top: 177px;
    left: 150px;
}

.mouth {
    position: absolute;
    width: 50px;
    height: 35px;
    top: 200px;
    left: 128px;
    background-color: var(--nose-eye-color);
    border-radius: 0 0 50% 50%/0 0 80% 80%;
}

.tongue-part-one {
    position: absolute;
    width: 30px;
    height: 15px;
    top: 13px;
    left: 10px;
    background-color: var(--tongue-color);
    border-radius: 0 0 50% 50%/0 0 80% 80%;
}

.tongue-part-two {
    position: absolute;
    width: 30px;
    height: 7px;
    top: 8px;
    left: 10px;
    background-color: var(--tongue-color);
    border-radius: 50% 50% 0 0/80% 80% 0 0;
}

.tongue-inner-part-one {
    position: absolute;
    width: 22.5px;
    height: 5px;
    top: 9.5px;
    left: 4px;
    background-color: var(--tongue-inner-color);
    border-radius: 0 0 50% 50%/0 0 100% 100%;
}

.tongue-inner-part-two {
    position: absolute;
    width: 22.5px;
    height: 6px;
    top: 4.8px;
    left: 4px;
    background-color: var(--tongue-inner-color);
    border-radius: 50% 50% 0 0 /100% 100% 0 0;
} */


.pineapple{
    background-color: #ffaa00;
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
    background-color: #ffaa00;
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

.leaf{
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
.leaf:before,
.leaf:after{
    position: absolute;
    content: "";
    background-color: #3f6b15;
    height: 70px;
    width: 70px;
    border-radius: 70px 0;
    transform-origin: right;
    top: -5px;

}

.leaf:before{
    transform: rotate(40deg);
    right: 20px;
    animation: leaf-right 0.5s infinite;
}
@keyframes leaf-right{
    50%{
        transform: rotate(32deg);
    }
}
.leaf:after{
    transform: rotate(-40deg);
    left: 20px;
    animation: leaf-left 0.5s infinite;
}
@keyframes leaf-left{
    50%{
        transform: rotate(-32deg);
    }
}

.leg-left,
.leg-right{
    height: 80px;
    width: 32px;
    background-color: #ffaa00;
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