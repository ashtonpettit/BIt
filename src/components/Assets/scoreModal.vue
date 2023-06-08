<template>
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
    </table>
    </div>

    </div>
</template>

<script>
import score_json from '../../assets/testfiles/gameScores.json';

export default{
data() {
    return{
        scores: {}

    }
},
mounted(){
    this.populateScoreboard();

},
methods: {
populateScoreboard(){
  let dict = {};
  score_json.forEach( x => {
    dict[x['date']] = x['Score']
  });

  this.scores = Object.entries(dict);
},
closeLeaderboard(){
  let modal = document.getElementById('gameModal');
  modal.style.display = 'none';
}

}
}

</script>
<style>
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