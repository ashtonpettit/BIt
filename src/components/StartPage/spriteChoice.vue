<template>
    <div id="images">
        <img id="nerdSprite" @click="this.showAlert('nerdSprite')" src="../../assets/images/sprite.png">
        <img id="summerSprite"  @click="this.showAlert('summerSprite')" src="../../assets/images/sprite.png">
        <img id="cuteSprite" @click="this.showAlert('cuteSprite')" src="../../assets/images/sprite.png">
        <img id="cheekySprite" @click="this.showAlert('cheekySprite')" src="../../assets/images/sprite.png">
        <img id="babySprite" @click="this.showAlert('babySprite')" src="../../assets/images/sprite.png">
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