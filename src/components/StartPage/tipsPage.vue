<template>
    <div id="test">
        <p v-if="this.currentTip == 1">
            Tip 1
            <br>Bit-Doodles need clean air! Unfortunately, human energy use is polluting the the Bit-Verse. To make sure your bit-doodle is healthy, try to use less energy.
        </p>
        <p v-if="this.currentTip == 2">
            Tip 2
            <br>Feed your bit-doodle! Your bit-doodle loves to eat {{ this.choice1 }} and {{ this.choice2 }}. Go to the shop and buy some for it. However you'll need Doodle Doolars to purchase with.
        </p>
        <p v-if="this.currentTip == 3">
            Tip 3
            <br>Need Doodle Doolars? Try competeing in the Sling Shot Game. Bit-Doodles looooove that game and reward the best players. However, your pollution will affect how easy or hard the game is.
            Using less energy will make earning Doodle Doolars much easier but you can still try even if you did pollute the Bit-Verse. That's how much Doodles love to watch you play!
        </p>
        <p v-if="this.currentTip == 3">Want to know how the Bit-Verse is being affected by your energy usage? Click on 'Data Breakdown' to learn more.</p>
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