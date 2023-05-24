
<template>
  <div id="buttonContainer">
      <button @click="this.init()">Play</button>
      <button @click="this.openLeaderboard()" id="myBtn">Leaderboard</button>
  </div>
  <div id="container">
    <div  v-if="this.gameStart" class="score current-score">
      score<br><span>{{currentScore}}</span>
    </div>
    
  </div>

</template>

<script>
import Matter from '../../../node_modules/matter-js';

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
const GRAVITY = 1.5;
const WIREFRAMES = false;
const BUMPER_BOUNCE = 1.5;


// shared variables
let currentScore, highScore;
let engine, world, render, stopperGroup;
let halfH, halfW;

export default {
  name: 'MiniGame',
  mounted() {
    this.currentScore = 0;
    this.highScore = 0;
  },
  data() {
    return {
      highScore : null,
      currentScore : null,
      gameStart: false,
      matterJsObjects: [],
      difficulty: null,
      vpHeight: 525,
      vpWidth: 1230
    }

  },
  methods: {
    openLeaderboard(){
      let modal = document.getElementById('myModal');
      modal.style.display = 'block';
    },
    init() {
      // Configuration and Display Settings
      this.difficulty = document.getElementById('DifficultyTracker').innerHTML;

      document.getElementById('sprite-container').style.display = 'none';
      document.getElementById('buttonContainer').style.display = 'none';
      console.log('Width: ' + this.vpWidth);
      console.log('Height: ' + this.vpHeight);
      

      // engine (shared)
      engine = Matter.Engine.create();

      // world (shared)
      world = engine.world;
      world.bounds = {
        min: { x: 0, y: 0},
        max: { x: this.vpWidth, y: this.vpHeight }
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
      if (this.difficulty == 'Easy'){
        this.createStaticBodiesEasy();
      }
      else if (this.difficulty == 'Medium'){
        this.createStaticBodiesMedium();
      }
      else {
        this.createStaticBodiesHard();
      }

      this.EventHandler();


    },
      createStaticBodiesMedium() {
      halfW = this.vpWidth/2;
      halfH = this.vpHeight/2;
      Matter.Composite.add(world, [
        // canvas boundaries (top, left, right)(x, y, width, height)
        this.boundary(halfW, -35, this.vpWidth, 100),
        this.boundary(-30, 400, 100, 800),
        this.boundary(this.vpWidth, 400, 35, 800),

        // drops (left, right)
        this.path(25, halfH, PATHS.DROP_LEFT),
        this.path(halfW * 1.95, halfH * 0.8, PATHS.DROP_RIGHT),

        //bumpers (x, y)
        // Altered by Difficulty
        this.bumper(halfW * 0.8, halfH * 1.3),
        this.bumper(halfW * 1.2, halfH/2),

        // obstacles    
        // Alter by Difficulty
        this.wall(halfW/2, halfH * 1.8, 300, 20, COLOR.INNER),
        this.wall(halfW * 1.5, halfH * 1.2, 200, 20, COLOR.INNER),

        //scoreZone
        this.scoreZone()

      ]);
    },
    createStaticBodiesEasy() {
      halfW = this.vpWidth/2;
      halfH = this.vpHeight/2;
      Matter.Composite.add(world, [
        // canvas boundaries (top, left, right)(x, y, width, height)
        this.boundary(halfW, -35, this.vpWidth, 100),
        this.boundary(-30, 400, 100, 800),
        this.boundary(this.vpWidth, 400, 35, 800),

        // drops (left, right)
        this.path(25, halfH, PATHS.DROP_LEFT),
        this.path(halfW * 1.95, halfH * 0.8, PATHS.DROP_RIGHT),

        // obstacles    
        // Alter by Difficulty
        this.wall(halfW/2, halfH * 1.8, 300, 20, COLOR.INNER),
        this.wall(halfW * 1.5, halfH * 1.2, 200, 20, COLOR.INNER),

        //scoreZone
        this.scoreZone()

      ]);
    },
    createStaticBodiesHard() {
      halfW = this.vpWidth/2;
      halfH = this.vpHeight/2;
      Matter.Composite.add(world, [
        // canvas boundaries (top, left, right)(x, y, width, height)
        this.boundary(halfW, -35, this.vpWidth, 100),
        this.boundary(-30, 400, 100, 800),
        this.boundary(this.vpWidth, 400, 35, 800),

        //bumpers (x, y)
        // Altered by Difficulty
        this.bumper(halfW * 0.8, halfH * 1.3),
        this.bumper(halfW * 1.2, halfH/2),

        // drops (left, right)
        this.path(25, halfH, PATHS.DROP_LEFT),
        this.path(halfW * 1.95, halfH * 0.8, PATHS.DROP_RIGHT),

        // obstacles    
        // Alter by Difficulty
        this.wall(halfW/2, halfH * 1.8, 300, 20, COLOR.INNER),
        this.wall(halfW * 1.5, halfH * 1.2, 200, 20, COLOR.INNER),

        //scoreZone
        this.scoreZone()

      ]);
    },
    plusPoints(score){
      this.currentScore += score;
    },
    EventHandler(){
      let self = this;
      halfW = this.vpWidth/2;
      halfH = this.vpHeight/2;
      //stacks
      let stack = this.stack(halfW * 0.30, halfH * 1.2, 8, 3);
      let pyramind = this.pyramid(halfW * 1.35, halfH * 0.5, 5, 4);
      let mouse = Matter.Mouse.create(render.canvas);
      let mouseConstraint = Matter.MouseConstraint.create(engine, {
            mouse:mouse,
            constraint: {
                render: {visible: false}
            }
        });
      let firing = false;
      //ball
      let ball = Matter.Bodies.circle(halfW * 0.4, halfH * 0.8, 15, {
        label: 'ball',
        collisionFilter: {
          group: stopperGroup
        },
        render: {
          fillStyle: COLOR.PINBALL
        }
      });

      let sling = Matter.Constraint.create({
        pointA: {x:halfW * 0.4, y:halfH * 0.8},
        bodyB: ball,
        stiffness: 0.08
      });

      Matter.Events.on(mouseConstraint, 'enddrag', function(e) {
            if(e.body === ball) firing = true;
      });
      let shots = 5;
      Matter.Events.on(engine, 'afterUpdate', function() {
                if(firing && Math.abs(ball.position.x- (halfW * 0.4)) < 20 && Math.abs(ball.position.y- (halfH * 0.8)) < 20) {
                    ball = Matter.Bodies.circle(halfW * 0.4, halfH * 0.8, 15, {
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
    return Matter.Bodies.rectangle(430, 930, 1500, 1, {
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