<template>
    <div id="images">
        <img id="nerdSprite" @click="this.showAlert('nerdSprite')" src="../../assets/images/nerdSprite.png">
        <img id="summerSprite"  @click="this.showAlert('summerSprite')" src="../../assets/images/summerSprite.png">
        <img id="cuteSprite" @click="this.showAlert('cuteSprite')" src="../../assets/images/cuteSprite.png">
        <img id="cheekySprite" @click="this.showAlert('cheekySprite')" src="../../assets/images/ghostSprite.png">
    </div>
</template>

<script>
export default {
  data () {
    return {
      previousSprite: null

    }
  },
  methods: {
    showAlert(spriteId){
      if (this.previousSprite == null) this.previousSprite = spriteId;
      else{
        document.getElementById(this.previousSprite).classList.remove('active');   
      }
      this.previousSprite = spriteId;
      document.getElementById(spriteId).className = 'active';
      document.getElementById('spriteTracker').innerHTML = spriteId;
    },

  }
}
</script>
