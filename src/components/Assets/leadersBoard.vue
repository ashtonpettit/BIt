<template>
    <!-- The Modal -->
    <div id="myModal" class="modal">

    <!-- Modal content -->
    <div class="modal-content">
    <span @click="this.closeLeaderboard()" class="close">&times;</span>
    <h3>Leaderboard</h3>
    <table id="dataTable">
        <tr>
        <th>Name</th>
        <th>Score</th>
        </tr>

        <tr v-for="(row, index) in this.leaderboard" :key="index">
        <td>{{ row[0] }}</td>
        <td>{{ row[1] }}</td>
        </tr>
    </table>
    </div>

    </div>
</template>

<script>
import leader_json from '../../assets/testfiles/leaderboard.json';

export default{
data() {
    return{
        leaderboard: {}

    }
},
mounted(){
    this.populateLeaderboard();

},
methods: {
populateLeaderboard(){
  let dict = {};
  leader_json.forEach( x => {
    dict[x['name']] = x['Score']
  });

  this.leaderboard = Object.entries(dict);
  this.leaderboard.sort((x, y) => y[1] - x[1]);
},
closeLeaderboard(){
  let modal = document.getElementById('myModal');
  modal.style.display = 'none';
}

}
}

</script>