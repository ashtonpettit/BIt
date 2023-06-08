<template>
    <div id="test">
        <p v-if="this.currentTip == 1">
            Tip 1
            <br>Bit-Doodles need clean air! Unfortunately, human energy use is polluting the the Bit-Verse. To make sure your bit-doodle is healthy, try to use less energy.
        </p>
        <p v-if="this.currentTip == 2">
            Tip 2
            <br>If you use more energy and the Bit-Verse is dirty- no problem! Try your shot at the slingshot game. The higher the score, the more pollution you clean up.
        </p>
        <p v-if="this.currentTip == 3">
            Tip 3
            <br>But the best way to make your Bit-Doodle happy is to try use less energy in your everyday life.
        </p>
        <button v-if="this.currentTip != 3" @click="this.nextTip()">Next Tip</button>
    </div>
</template>

<script>
    export default{
        data(){
            return {
                foodChoices: [
                    'Strawberries',
                    'Marshmellows',
                    'Cookies',
                    'Broccoli',
                    'Potatoes'
                ],
                choice1: null,
                choice2: null,
                currentTip: 1

            }
        },
        mounted() {
            this.favFood();
        },
        methods: {
            favFood(){
                let num = this.randomNumber();
                console.log(num);
                this.choice1 = this.foodChoices[num];
                console.log(this.choice1);
                num = this.randomNumber();
                this.choice2 = this.foodChoices[num];
                console.log(this.choice2);
            },
            randomNumber(){
                let random = Math.floor(Math.random()*this.foodChoices.length);
                return random;
            },
            nextTip() {
                this.currentTip += 1;
            }
        }
    }

</script>