<template>
  <div class="main-page">
    <div class="main-page-header">
      <h1 class="header-title">Vale's Coffee Nest</h1>
    </div>
    <div class="coffee-main-section">
      Seleccione su café de preferencia:
      <div class="coffee-list">
        <div class="coffee-container" v-for="coffee in coffeeList" :key="coffee.name">
          <div class="coffee-image">
            <img :src="getImage(coffee.image)" alt="Coffee" width="200" height="200">
          </div>
          <div class="coffee-info">
            <div class="coffee-name"> {{ coffee.coffeeType }} </div>
            <div> ₡{{ coffee.price }} </div>
            <div class="coffee-quantity"> Disponibles: {{ coffee.quantity }} </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import { BACKEND_URL } from "@/config";


export default {
  name: 'MainPage',
  data() {
    return {
      coffeeList: []
    }
  },
  methods: {
    async getCoffeeList() {
      try {
        const response = await axios.get(`${BACKEND_URL}/Coffee`);
        this.coffeeList = response.data;
        console.log(this.coffeeList);
      } catch (error) {
        console.error(error)
      }
    },
    getImage(imagePath) {
      const url = `https://localhost:7263${imagePath}`;
      return url;
    }
  },
  mounted() {
    this.getCoffeeList();
  }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&family=Mooli&display=swap');

.main-page-header {
  background-color: #310603;
  padding: 40px;
  margin: 0;
}

.header-title {
  color: #ffffff;
  text-align: center;
  font-family: "Mooli", sans-serif;
  font-weight: 100;
  font-size: 2.5em;
  margin: 0;
}

.coffee-main-section {
  font-family: "Montserrat", sans-serif;
  font-weight: 400;
  font-size: 30px;
  text-align: center;
  margin-top: 30px;
}

.coffee-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

.coffee-container {
  display: flex;
  background-color: #A18363;
  width: 550px;
  height: 260px;
  margin-top: 20px;
  margin-right: 65px;
  margin-left: 65px;
  border-radius: 20px;
  align-items: center;
}

.coffee-info {
  margin-left: 30px;
}

.coffee-image {
  margin-left: 25px
}

.coffee-name {
  font-size: 39px;
  font-weight: 500;
  text-align: center;
  margin-top: 10px;
  margin-bottom: 20px;
}

.coffee-quantity {
  font-size: 20px;
  font-weight: 400;
  text-align: center;
  margin-top: 20px;
}

</style>
