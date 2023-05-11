
<template>
  <div id="buttonContainer">
      <button @click="this.init()">Play</button>
      <button @click="this.openLeaderboard()" id="myBtn">Leaderboard</button>
  </div>
  <div id="container">
    <div  v-if="this.gameStart" class="score current-score">
      score<br><span>{{currentScore}}</span>
    </div>

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
    
  </div>

</template>

<script>
import Matter from '../../../node_modules/matter-js';
import leader_json from '../../assets/testfiles/leaderboard.json';

const PATHS = {
    DROP_LEFT: '0 0 20 0 70 100 20 150 0 150 0 0',
    DROP_RIGHT: '50 0 68 0 68 150 50 150 0 100 50 0',
    APRON_LEFT: '0 0 180 120 0 120 0 0',
    APRON_RIGHT: '180 0 180 120 0 120 180 0'
  };

const COLOR = {
    BACKGROUND: '#212529',
    OUTER: '#495057',
    INNER: '#15aabf',
    BUMPER: '#fab005',
    BUMPER_LIT: '#fff3bf',
    PADDLE: '#e64980',
    PINBALL: '#dee2e6'
  };
const GRAVITY = 0.75;
const WIREFRAMES = false;
const BUMPER_BOUNCE = 1.5;


// shared variables
let currentScore, highScore;
let engine, world, render, stopperGroup;

export default {
  name: 'MiniGame',
  mounted() {
    this.currentScore = 0;
    this.highScore = 0;
    this.populateLeaderboard();
  },
  data() {
    return {
      highScore : null,
      currentScore : null,
      gameStart: false,
      leaderboard: {},
      matterJsObjects: []
    }

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
    openLeaderboard(){
      let modal = document.getElementById('myModal');
      modal.style.display = 'block';
    },
    closeLeaderboard(){
      let modal = document.getElementById('myModal');
      modal.style.display = 'none';
    },
    init() {
      // engine (shared)
      engine = Matter.Engine.create();

      // world (shared)
      world = engine.world;
      world.bounds = {
        min: { x: 0, y: 0},
        max: { x: 850, y: 550 }
      };
      world.gravity.y = GRAVITY; // simulate rolling on a slanted table

      // render (shared)
      render = Matter.Render.create({
        element: document.getElementById("container"),
        engine: engine,
        options: {
          width: world.bounds.max.x,
          height: world.bounds.max.y,
          wireframes: WIREFRAMES,
          background: COLOR.BACKGROUND
        }
      });
      Matter.Render.run(render);

      // runner
      let runner = Matter.Runner.create();
      Matter.Runner.run(runner, engine);

      stopperGroup = Matter.Body.nextGroup(true);
      this.gameStart = true;
      this.createStaticBodies();  
      this.EventHandler();


    },
      createStaticBodies() {
      Matter.Composite.add(world, [
        // canvas boundaries (top, bottom, left, right)
        this.boundary(430, -35, 830, 100),
        this.boundary(-30, 400, 100, 800),
        this.boundary(850, 400, 35, 800),
        //bumpers
        this.bumper(165, 240),
        this.bumper(650, 440),

        // drops (left, right)
        this.path(25, 360, PATHS.DROP_LEFT),
        this.path(830, 130, PATHS.DROP_RIGHT),

        // obstacles    
        this.wall(330, 510, 300, 20, COLOR.INNER),
        this.wall(620, 220, 200, 20, COLOR.INNER),

        //scoreZone
        this.scoreZone()

      ]);
    },
    plusPoints(score){
      this.currentScore += score;
    },
    pingBumper(bumper) {
    console.log(bumper);
    this.updateScore(currentScore + 10);

    //flash color
    bumper.render.fillStyle = COLOR.BUMPER_LIT;
    setTimeout(function() {
      bumper.render.fillStyle = COLOR.BUMPER;
    }, 100);
  },
    EventHandler(){
      let self = this;
      //stacks
      let stack = this.stack(200, 360, 8, 4);
      let pyramind = this.pyramid(540, 100, 5, 4);
      let mouse = Matter.Mouse.create(render.canvas);
      let mouseConstraint = Matter.MouseConstraint.create(engine, {
            mouse:mouse,
            constraint: {
                render: {visible: false}
            }
        });
      let firing = false;
      //ball
      let ball = Matter.Bodies.circle(200, 100, 15, {
        label: 'ball',
        collisionFilter: {
          group: stopperGroup
        },
        render: {
          fillStyle: COLOR.PINBALL
        }
      });

      let sling = Matter.Constraint.create({
        pointA: {x:200, y:100},
        bodyB: ball,
        stiffness: 0.03
      });

      Matter.Events.on(mouseConstraint, 'enddrag', function(e) {
            if(e.body === ball) firing = true;
      });
      let shots = 5;
      Matter.Events.on(engine, 'afterUpdate', function() {
                if(firing && Math.abs(ball.position.x-200) < 20 && Math.abs(ball.position.y-100) < 20) {
                    ball = Matter.Bodies.circle(200, 100, 15, {
                      label: 'ball',
                      collisionFilter: {
                        group: stopperGroup
                      },
                      render: {
                        fillStyle: COLOR.PINBALL
                      }
                    });
                    shots = shots - 1;
                    if (shots > 0){
                      Matter.Composite.add(world, ball);
                    }
                    
                    sling.bodyB = ball;
                    firing = false;
                }
            });

       // events for when stackitems hit the scoreZone
       Matter.Events.on(engine, 'collisionStart', function(event){
        let pairs = event.pairs;
        pairs.forEach(function(pair){
          if (pair.bodyA.label === 'scorezone'){
            self.updateScore(5, pair.bodyB.id);
          }
        })
       });
       

       // events for when the pinball hits stuff
       Matter.Events.on(engine, 'collisionStart', function(event) {
        let pairs = event.pairs;
        
        pairs.forEach(function(pair) {
          if (pair.bodyB.label === 'ball') {
            switch (pair.bodyA.label) {
              case 'stackItem':
                break;
              case 'bumper':
                self.plusPoints(50);    
                //flash color
                pair.bodyA.render.fillStyle = COLOR.BUMPER_LIT;
                setTimeout(function() {
                  pair.bodyA.render.fillStyle = COLOR.BUMPER;
                }, 100);
                  break;
            }         
          }
        });
      });
      Matter.Composite.add(world, [ball, sling, mouseConstraint, stack, pyramind]);
      

    },
  updateScore(score, id) {
    let object = Matter.Composite.get(world, id, 'body');
    if (!this.matterJsObjects.includes(object.id)){
      this.matterJsObjects.push(object.id);
      this.currentScore += score;
      highScore = Math.max(currentScore, highScore);
      this.highScore = highScore;
    }
  },

  // matter.js has a built in random range function, but it is deterministic
  rand(min, max) {
    return Math.random() * (max - min) + min;
  },

  // outer edges of pinball table
  boundary(x, y, width, height) {
    return Matter.Bodies.rectangle(x, y, width, height, {
      isStatic: true,
      render: {
        fillStyle: COLOR.OUTER
      }
    });
  },

  // wall segments
  wall(x, y, width, height, color, angle = 0) {
    return Matter.Bodies.rectangle(x, y, width, height, {
      angle: angle,
      isStatic: true,
      chamfer: { radius: 10 },
      render: {
        fillStyle: color
      }
    });
  },

  // bodies created from SVG paths
  path(x, y, path) {
    let vertices = Matter.Vertices.fromPath(path);
    return Matter.Bodies.fromVertices(x, y, vertices, {
      isStatic: true,
      render: {
        fillStyle: COLOR.OUTER,

        // add stroke and line width to fill in slight gaps between fragments
        strokeStyle: COLOR.OUTER,
        lineWidth: 1
      }
    });
  },

  // round bodies that repel pinball
  bumper(x, y) {
    let bumper = Matter.Bodies.circle(x, y, 25, {
      label: 'bumper',
      isStatic: true,
      render: {
        fillStyle: COLOR.BUMPER
      }
    });

    // for some reason, restitution is reset unless it's set after body creation
    bumper.restitution = BUMPER_BOUNCE;

    return bumper;
  },

  ball(x, y){
    let ball = Matter.Bodies.circle(x, y, 15);
    return ball;
  },

  scoreZone() {
    return Matter.Bodies.rectangle(430, 930, 1000, 1, {
      label: 'scorezone',
      isStatic: true,
      render: {
        fillStyle: '#fff'
      }
    });
  },
  stack(x, y, column, row){
    let stack = Matter.Composites.stack(x, y, column, row, 0, 0, function(x, y) {
      return Matter.Bodies.polygon(x, y, 8, 18, {
        label: 'stackItem',
        render: {
          fillStyle: 'FFFF00'
        }
      });
    });
    return stack;
  },
  pyramid(x, y, column, row){
    let pyramid = Matter.Composites.pyramid(x, y, column, row, 0, 0, function(x, y){
      return Matter.Bodies.polygon(x, y, 8, 18);
    });
    return pyramid;
  }
  }
}

</script>

<style lang="css">

@import '../../assets/css/stylesheet.css';


</style>