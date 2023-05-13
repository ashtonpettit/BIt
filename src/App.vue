<template>
  <p style="display:none" id="CalculatedVolume"></p>
  <p style="display: none" id="spriteTracker">0</p>
  <p style="display: none;" id="DifficultyTracker">Easy</p>

  <!-- Sprites and MiniGame Assets -->
  <NerdSprite v-if="this.spriteID == 'nerdSprite'"></NerdSprite>
  <SummerSprite v-if="this.spriteID == 'summerSprite'"></SummerSprite>
  <CuteSprite v-if="this.spriteID == 'cuteSprite'"></CuteSprite>
  <CheekySprite v-if="this.spriteID == 'cheekySprite'"></CheekySprite>
  <MiniGameMedium v-if="this.currentPage == 5"></MiniGameMedium>
  <Leaderboard></Leaderboard>

  <!--Starting Pages containing instructions and Sprite Selection-->
  <div v-if="this.currentPage < 4" id="startPage-container">

      <!--Sprite Selection Page-->
        <div id="picture-container" class="child-container">
            <div id="title"><h1>BIT ENERGY</h1></div>
            <SpriteChoice v-if="currentPage == 1"></SpriteChoice>
        </div>
        
        <!--Explanation of Sprite Rules with Tips-->
        <div id="text-container" class="child-container">
            <ExplainChoice v-if="this.currentPage == 1"></ExplainChoice>
            <div id="explain" v-if="this.currentPage == 2">
                <p>Say Hello to your new Bit-Doodle, {{ this.bitFriend }}!</p>
                <p>{{ this.bitFriend }} is your responsibility. Make sure to take care of {{ this.pronoun }}. If you already know how Bit Energy works, feel free to skip.
                Otherwise, click 'Explore' to learn all about the Bit-Verse!</p>
            </div>
            <TipsPage v-if="this.currentPage == 3"></TipsPage>
        </div>

        <!--Navigation Buttons-->
        <div class="child-container">
            <div v-if="this.currentPage == 2" id="button-container">
                <button @click="this.buttonClicked(2)">Explore</button>
                <button>Skip</button>
            </div>
            <div v-if="this.currentPage == 3">
                <button @click="this.bitVerseInit()">Go to Bit-Verse</button>
                <button>Data Breakdown</button>
            </div>
            <div v-if="this.currentPage == 1" id="confirm-container">
                <button @click="this.buttonClicked(2)">Confirm</button>
            </div>
        </div>
    </div>
</template>

<script>

import MiniGameMedium from "./components/MiniGame/minigameMedium.vue"
import SummerSprite from "./components/Sprite/SummerSprite.vue"
import CuteSprite from "./components/Sprite/CuteSprite.vue"
import my_json from "./assets/testfiles/energyData.json"
import ExplainChoice from "./components/StartPage/explainChoice.vue";
import TipsPage from "./components/StartPage/tipsPage.vue";
import SpriteChoice from "./components/StartPage/spriteChoice.vue";
import NerdSprite from "./components/Sprite/NerdSprite.vue";
import CheekySprite from "./components/Sprite/CheekySprite.vue";
import Leaderboard from "./components/Assets/leadersBoard.vue";


export default {
  name: 'App',
  components: {
    SummerSprite,
    CuteSprite,
    MiniGameMedium,
    ExplainChoice,
    TipsPage,
    SpriteChoice,
    NerdSprite,
    CheekySprite,
    Leaderboard
    
  },
  data() {
    return {
      outputToday : 0,
      outputPrev : 0,
      currentPage: 5,
      showSprite: false,
      bitFriend: 'Milo',
      pronoun: 'him',
      newSession: false,
      spriteID: 'nerdSprite'

    }
  },
  mounted() {
    this.formatDates();
    this.checkSession();
  },
  methods: {
    getPrevDate(date, days){
      let day = date;
      day.setDate(day.getDate()-days);
      return day.toLocaleDateString();
    },
    initPage(today, formatPrevDate){
      let todayOutput;
      let prevOutput;


      my_json.forEach( x => {if (x['Van Datum'] == today) if (x['Register'] == 'Afname Dag'){ todayOutput = x['Volume']}});
      my_json.forEach( x => {if (x['Van Datum'] == formatPrevDate) if (x['Register'] == 'Afname Dag'){ prevOutput = x['Volume']}});

      console.log(today + " " + formatPrevDate);
      document.getElementById("CalculatedVolume").innerHTML = todayOutput - prevOutput;
      this.outputToday = todayOutput;
      this.outputPrev = prevOutput;
    },
    formatDates(){
      let today = "18-10-2022";
      let date = today.split('-');
      let newdate = new Date(date[2] + "-" + date[1] + "-" + date[0]);
      let prevDay = this.getPrevDate(newdate, 1).split("/");
      let formatPrevDate = prevDay[1] + "-" + prevDay[0] + "-" + prevDay[2];

      //send to INIT
      this.initPage(today, formatPrevDate);

    },
    checkSession(){
      //database connection here.
      if (this.newSession == true){
        this.currentPage = 1;
        this.newSession = false;
      }
      else {
        this.currentPage = 5;
      }

    },
    bitVerseInit(){
      this.spriteID = document.getElementById('spriteTracker').innerHTML;
      console.log(this.spriteID);
      this.currentPage = 5;
    },
    buttonClicked(buttonId){
            if (buttonId == 2){
                let id = document.getElementById("spriteTracker").innerHTML;
                if (id == 0){
                    alert("Please Choose a Sprite");
                }
                else {
                    this.currentPage += 1;
                    this.showSprite = true;
                }
            }
            else {
                this.currentPage += 1;
            }
        }

  }
  
}

</script>
