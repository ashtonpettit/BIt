<template>
<div id="root">
  <p style="display:none" id="SessionTracker"></p>
  <p style="display: none" id="spriteTracker">0</p>
  <p style="display: none;" id="DifficultyTracker"></p>
  <p style="display: none;" id="Score"></p>
  <p style="display: none;" id="Date"></p>

  <!-- Sprites and MiniGame Assets -->
  <NerdSprite v-if="this.spriteID == 'nerdSpriteEasy'"></NerdSprite>
  <SadNerdSprite v-if="this.spriteID == 'nerdSpriteHard'"></SadNerdSprite>
  <SummerSprite v-if="this.spriteID == 'summerSpriteEasy'"></SummerSprite>
  <CuteSprite v-if="this.spriteID == 'cuteSpriteEasy'"></CuteSprite>
  <CheekySprite v-if="this.spriteID == 'cheekySpriteEasy'"></CheekySprite>
  <MiniGameMedium v-if="this.currentPage == 5"></MiniGameMedium>

      <!-- The Modal -->
  <div id="gameModal" class="gameOverModal">
  <!-- Modal content -->
  <div class="game-modal-content">
  <span @click="this.closeModal()" class="close">&times;</span>
  <h3>Game Over</h3>
  <table id="dataTable">
      <tr>
      <th>Date</th>
      <th>Score</th>
      </tr>
      <tr v-for="(row, index) in this.scores" :key="index">
      <td>{{ row[0] }}</td>
      <td>{{ row[1] }}</td>
      </tr>
      <button id="unlockScore" @click="this.unlockScore()">Unlock Scores</button>
      <tr v-if="this.gameEnd">
        <td>{{ this.date}}</td>
        <td>{{ this.score }}</td>
      </tr>
  </table>
  </div>

  </div>

  <!--Starting Pages containing instructions and Sprite Selection-->
  <div v-if="this.currentPage < 4" id="startPage-container">

      <!--Sprite Selection Page-->
        <div id="picture-container" class="child-container">
            <SpriteChoice v-if="currentPage == 1"></SpriteChoice>
        </div>
        <div id="title"></div>
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
                <button @click="this.buttonClicked(2)">Skip</button>
            </div>
            <div v-if="this.currentPage == 3">
                <button @click="this.bitVerseInit()">Go to Bit-Verse</button>
            </div>
            <div v-if="this.currentPage == 1" id="confirm-container">
                <button @click="this.buttonClicked(2)">Confirm</button>
            </div>
        </div>
    </div>

  </div>
  
</template>

<script>

// Game Import
import MiniGameMedium from "./components/MiniGame/minigameMedium.vue";

//Sprites Happy Import
import SummerSprite from "./components/Sprite/SummerSprite/SummerSprite.vue";
import CuteSprite from "./components/Sprite/CuteSprite/CuteSprite.vue";
import NerdSprite from "./components/Sprite/NerdSprite/NerdSprite.vue";
import CheekySprite from "./components/Sprite/CheekySprite/CheekySprite.vue";

//Sprites Sad Import
import SadNerdSprite from "./components/Sprite/NerdSprite/SadNerdSprite.vue";

// Components Import
import ExplainChoice from "./components/StartPage/explainChoice.vue";
import TipsPage from "./components/StartPage/tipsPage.vue";
import SpriteChoice from "./components/StartPage/spriteChoice.vue";
import score_json from './assets/testfiles/gameScores.json';

export default {
  name: 'App',
  components: {
    SummerSprite,
    CuteSprite,
    SadNerdSprite,
    MiniGameMedium,
    ExplainChoice,
    TipsPage,
    SpriteChoice,
    NerdSprite,
    CheekySprite
    
  },
  data() {
    return {
      currentPage: 1,
      showSprite: false,
      bitFriend: 'Milo',
      sessionVerify: false,
      pronoun: 'him',
      spriteID: null,
      energyData: [],
      currentDayData: '',
      prevDate: '',
      len: 0,
      scores: {},
      gameEnd: false,
      date: "17/11/1993",
      score: 20
    }
  },
  async mounted() {
    await this.loadData();
    await this.formatDates();
  },
  methods: {
    async loadData(){
      const apiurl = "http://localhost:5118/api/Electric";
      const response = await fetch(apiurl);
      this.energyData = await response.json();
      this.len = this.energyData.length - 1;

    },
    unlockScore(){
      document.getElementById("unlockScore").style.display = 'none';
      this.score = document.getElementById("Score").innerHTML;
      this.date = document.getElementById("Date").innerHTML;
      this.gameEnd = true;
    },
    getPrevDate(date){
      date.setFullYear(date.getFullYear() - 1);
      date = date.toLocaleDateString().split('/');
      return date;
    },
    initPage(PrevDate){
      let dayOutput = this.energyData[this.len - 1].value + this.currentDayData.value;
      let prevOutput;
      this.energyData.forEach( x => {if (x['date'] == PrevDate) if (x['register'] == 'Day'){ prevOutput = x['value']}});
      this.energyData.forEach( x => {if (x['date'] == PrevDate) if (x['register'] == 'Night'){ prevOutput += x['value']}});

      if (dayOutput > prevOutput){
        document.getElementById("DifficultyTracker").innerHTML = 'Hard';   
      }
      else {
        document.getElementById("DifficultyTracker").innerHTML = 'Easy';
      }

      
    },
    formatDates(){     
      this.currentDayData = this.energyData[this.len]
      let date = this.currentDayData.date.split('-');
      let newdate = new Date(date[2] + "-" + date[1] + "-" + date[0]);
      let prevDay = this.getPrevDate(newdate);
      this.prevDate = prevDay[2] + "-0" + prevDay[1] + "-0" + prevDay[0];
      this.populateScoreboard();
      //send to INIT
      this.initPage(this.prevDate);

    },
    bitVerseInit(){
      let sprite= document.getElementById('spriteTracker').innerHTML;
      let difficulty = document.getElementById('DifficultyTracker').innerHTML;
      difficulty = 'Hard';
      this.spriteID = sprite + difficulty;
      this.currentPage = 5;
      if (difficulty === 'Hard'){
        let element = document.getElementById('root');
        element.setAttribute("id", "smoky");
        alert('Uh Oh! Looks like you used more energy than this time last year. No problem! Play the game and try to lower the pollution by scoring points!');
      }
      else {
        alert('Well Done! You used less energy than this time last year. Thanks for keeping the bit-verse clean :)');
      }
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
        },
        populateScoreboard(){
          let dict = {};
          console.log(score_json);
          score_json.forEach( x => {
            dict[x['date']] = x['Score']
          });

          this.scores = Object.entries(dict);
        },
        closeModal(){
          let modal = document.getElementById('gameModal');
          let element = document.getElementById('smoky');
          modal.style.display = 'none';
          let sprite = document.getElementById("spriteTracker").innerHTML;
          this.spriteID = sprite;
          element.setAttribute("id", "root");
        }

  }
  
}

</script>

<style lang="scss">
@import url('https://fonts.googleapis.com/css2?family=Nunito:wght@300&display=swap');

/* --- Universal Box Sizing with Inheritance --- */
html {
  box-sizing: border-box;
}
*,
*:before,
*:after {
  box-sizing: inherit;
}

/* --- Styles --- */
body {
  margin: 0;
  min-height: 100vh;
  position: relative;
  overflow: hidden;
}

#smoky {
  width: 100vw;
  height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  background: url('./assets/images/smoke_city.png');
  background-size: cover;
  background-repeat: no-repeat;
}

#root {
  width: 100vw;
  height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
  background:
    radial-gradient(circle at 19% 2%, #ffdcd8 0.08%, transparent 0.2%),
    radial-gradient(circle at 39% 5%, #ffdcd8 0.05%, transparent 0.2%),
    radial-gradient(circle at 15% 6%, #ffdcd8 0.08%, transparent 0.35%),
    radial-gradient(circle at 89% 22%, #ffdcd8 0.05%, transparent 0.2%),
    radial-gradient(circle at 19% 26%, #ffdcd8 0.1%, transparent 0.3%),
    radial-gradient(circle at 34% 24%, #ffdcd8 0.05%, transparent 0.2%),
    radial-gradient(circle at 79% 17%, #ffdcd8 0.1%, transparent 0.3%),
    radial-gradient(circle at 36% 10%, #ffdcd8 0.03%, transparent 0.4%),
    radial-gradient(circle at 48% 6%, #ffdcd8 0.1%, transparent 0.3%),
    radial-gradient(circle at 2% 9%, #ffdcd8 0.05%, transparent 0.2%),
    radial-gradient(circle at 10% 33%, #ffdcd8 0.03%, transparent 0.15%),
    radial-gradient(circle at 55% 14%, #ffdcd8 0.05%, transparent 0.2%),
    radial-gradient(circle at 30% 19%, #ffdcd8 0.1%, transparent 0.3%),
    radial-gradient(circle at 79% 5%, #ffdcd8 0.05%, transparent 0.2%),
    radial-gradient(circle at 64% 18%, #ffdcd8 0.02%, transparent 0.4%),
    radial-gradient(circle at 85% 32%, #ffdcd8 0.02%, transparent 0.25%),
    // stars-water
    radial-gradient(circle at 19% 98%, #986962 0.08%, transparent 0.2%),
    radial-gradient(circle at 39% 95%, #986962 0.05%, transparent 0.2%),
    radial-gradient(circle at 15% 94%, #986962 0.08%, transparent 0.35%),
    radial-gradient(circle at 89% 78%, #986962 0.05%, transparent 0.2%),
    radial-gradient(circle at 19% 74%, #986962 0.1%, transparent 0.3%),
    radial-gradient(circle at 34% 76%, #986962 0.05%, transparent 0.2%),
    radial-gradient(circle at 79% 83%, #986962 0.1%, transparent 0.3%),
    radial-gradient(circle at 36% 90%, #986962 0.03%, transparent 0.4%),
    radial-gradient(circle at 48% 94%, #986962 0.1%, transparent 0.3%),
    radial-gradient(circle at 2% 91%, #986962 0.05%, transparent 0.2%),
    radial-gradient(circle at 10% 67%, #986962 0.03%, transparent 0.15%),
    radial-gradient(circle at 55% 86%, #986962 0.05%, transparent 0.2%),
    radial-gradient(circle at 30% 81%, #986962 0.1%, transparent 0.3%),
    radial-gradient(circle at 79% 95%, #986962 0.05%, transparent 0.2%),
    radial-gradient(circle at 64% 82%, #986962 0.02%, transparent 0.4%),
    radial-gradient(circle at 85% 68%, #986962 0.02%, transparent 0.25%),
    // colours
    radial-gradient(
        transparent,
        rgba(#fb6c59, 0.2),
        rgba(#78849f, 0.2),
        rgba(#29304d, 0.8)
      ),
    linear-gradient(
      #65809d,
      #6e80a9,
      #fdd3ab,
      #ffa980,
      #fd695d 50%,
      #371038 50.1%,
      #363d79,
      #45225c,
      #1e0410
    );
  filter: saturate(1.25);
}

#title {
  &::before,
  &::after {
    content: 'Welcome to the Bit-Verse';
    height: 46%;
    font-family: 'Nunito', sans-serif;
    font-size: 9vmin;
    color: #f9ccca;
    display: flex;
    justify-content: center;
    align-items: center;
    text-shadow: 0 0 5px #eea9e3, 0 0 15px #eea9e3, 0 0 25px #eea9e3, 0 0 35px #eea9e3, 0 0 50px #d157a7,
      0 0 75px #d157a7, 0 0 100px #d157a7, 0 0 150px #d157a7;
    animation: flicker 1.5s infinite alternate;
  }

  &::after {
    transform: rotateX(180deg) translateY(32%);
    filter: blur(3px) opacity(50%);
  }
}

@keyframes flicker {
  0%,
  16%,
  18%,
  21%,
  23%,
  54%,
  56%,
  100% {
    text-shadow: 0 0 5px #eea9e3, 0 0 15px #eea9e3, 0 0 25px #eea9e3, 0 0 35px #eea9e3, 0 0 50px #d157a7,
      0 0 75px #d157a7, 0 0 100px #d157a7, 0 0 150px #d157a7;
  }

  17%,
  22%,
  55% {
    text-shadow: 0 0 5px #eea9e3, 0 0 5px #eea9e3, 0 0 5px #eea9e3, 0 0 15px #eea9e3, 0 0 30px #d157a7,
      0 0 45px #d157a7, 0 0 75px #d157a7, 0 0 125px #d157a7;
  }
}

/* The Modal (background) */
#gameModal {
  display: none; /* Hidden by default */
  position: absolute; 
  z-index: 1; 
  left: 50%;
  top: 50%;
  width: 100%;
  transform: translate(-50%, -50%);
  overflow: auto; 
  background-color: rgb(0,0,0); 
  background-color: rgba(0,0,0,0.4);
}

/* Modal Content/Box */
.game-modal-content {
  background-color: #fefefe;
  text-decoration-color: #212529;
  margin: 15% auto; /* 15% from the top and centered */
  padding: 20px;
  border: 1px solid #888;
  width: 60%; /* Could be more or less, depending on screen size */
}

#dataTable {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
}
th, td {
  border: 1px solid #ddd;
  padding: 8px;
}
#dataTable tr:nth-child(even){background-color: #f2f2f2;}
#dataTable tr:hover {background-color: #ddd;}
#dataTable th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #04AA6D;
  color: white;
}


h3, th, td {
  color: #212529;
}

/* The Close Button */
.close {
  color: #aaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: black;
  text-decoration: none;
  cursor: pointer;
}

</style>

