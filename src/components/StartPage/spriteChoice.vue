<template>
    <div id="images">
        <img id="spriteBlack" @click="this.showAlert(1)" src="../../assets/images/sprite.png">
        <img id="spriteYellow"  @click="this.showAlert(2)" src="../../assets/images/sprite.png">
        <img id="spritePink" @click="this.showAlert(3)" src="../../assets/images/sprite.png">
        <img id="spriteGreen" @click="this.showAlert(4)" src="../../assets/images/sprite.png">
        <img id="spriteblue" @click="this.showAlert(5)" src="../../assets/images/sprite.png">
    </div>
</template>

<script>
export default {
  data () {
    return {

    }
  },
  methods: {
    showAlert(choice){
        document.getElementById('spriteTracker').innerHTML = choice;


    },

  }
}
</script>