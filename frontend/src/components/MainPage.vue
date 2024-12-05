<template>
  <div :class="{ 'blur-background': isAddModalVisible }" class="main-page">
    <div class="main-page-header">
      <h1 class="header-title">Vale's Coffee Nest</h1>
      <div class="header-info">
        <div class="total-price">
          Total: ₡{{ priceTotal }}
        </div>
        <div class="order-header">Ver mi orden</div>
      </div>
    </div>
    <div class="coffee-main-section">
      Seleccione su café de preferencia:
      <div class="coffee-list">
        <div class="coffee-container" v-for="coffee in coffeeList" :key="coffee.name" @click="selectCoffee(coffee)">
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

  <ModalComponent
    :isVisible="isAddModalVisible"
    title="Agregar a la orden"
    @close="isModalVisible = false"
  >
    <template #body>
      <div v-if="selectedCoffee" class="modal-content">
        <h2>{{ selectedCoffee.coffeeType }}</h2>
        <img :src="getImage(selectedCoffee.image)" alt="Coffee Image" width="200" height="200">
        <p class="coffee-price-modal">₡{{ selectedCoffee.price * quantity}}</p>
        Ingrese la cantidad deseada:
        <div class="input-group-modal">
          <svg @click="decreaseQuantity" xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-dash-circle" viewBox="0 0 16 16">
            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
            <path d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8"/>
          </svg>
          <input type="number" min="1" :max="selectedCoffee.quantity" v-model="quantity" placeholder="1" class="input-field"/>
          <svg @click="increaseQuantity" xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
          </svg>
      </div>
      </div>
    </template>
    <template v-slot:footer>
      <button @click="addCoffee" type="button" class="custom-button custom-font white-font">Agregar</button>
      <button @click="isAddModalVisible = false" type="button" class="custom-button custom-font white-font">Cerrar</button>
    </template>
  </ModalComponent>

</template>

<script>
import axios from 'axios'
import { BACKEND_URL } from "@/config";
import ModalComponent from "./ModalComponent.vue";


export default {
  name: 'MainPage',
  components: {
    ModalComponent
  },
  data() {
    return {
      coffeeList: [],
      selectedCoffees: [],
      isAddModalVisible: false,
      selectedCoffee: null,
      quantity: 1,
      priceTotal: 0,
    }
  },
  methods: {
    async getCoffeeList() {
      try {
        const response = await axios.get(`${BACKEND_URL}/Coffee`);
        this.coffeeList = response.data;
      } catch (error) {
        console.error(error)
      }
    },
    getImage(imagePath) {
      const url = `https://localhost:7263${imagePath}`;
      return url;
    },
    showModal() {
      this.isAddModalVisible = true;
      console.log("Modal triggered");
    },
    selectCoffee(coffee) {
      this.selectedCoffee = coffee;
      this.quantity = 1;
      this.showModal();
    },
    increaseQuantity() {
      if (this.quantity < this.selectedCoffee.quantity) {
        this.quantity++;
      } else {
        alert('No more coffee available for this type!');
      }
    },
    decreaseQuantity() {
      if (this.quantity > 0) {
        this.quantity--;
      }
    },
    addCoffee() {
    if (this.selectedCoffee.quantity >= this.quantity) {
      // Deduct the selected quantity from the available stock
      this.selectedCoffee.quantity -= this.quantity;

      // Add or update the coffee in the selectedCoffees array
      const existingCoffee = this.selectedCoffees.find(
        (item) => item.coffeeType === this.selectedCoffee.coffeeType
      );

      if (existingCoffee) {
        existingCoffee.quantity += this.quantity;
      } else {
        this.selectedCoffees.push({
          coffeeType: this.selectedCoffee.coffeeType,
          price: this.selectedCoffee.price,
          quantity: this.quantity,
        });
      }

      this.priceTotal += this.selectedCoffee.price * this.quantity;
      this.isAddModalVisible = false;

      alert('Coffee added to the order!');
    } else {
      alert('Not enough stock available!');
    }
  },

  },
  mounted() {
    this.getCoffeeList();
  }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&family=Mooli&display=swap');

.blur-background {
  filter: blur(5px);
  opacity: 0.7;
}

.main-page-header {
  display: flex;
  background-color: #310603;
  justify-content: space-between;
  align-items: center;
  padding: 32px;
  margin: 0;
  position: relative;
}

.header-title {
  color: #ffffff;
  text-align: center;
  font-family: "Mooli", sans-serif;
  font-weight: 100;
  font-size: 2.5em;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  margin: 0;
}

.header-info {
  margin-left: auto;
  display: flex;
  align-items: center;
  flex-direction: column;
}

.total-price {
  color: #ffffff;
  font-family: "Montserrat", sans-serif;
  font-weight: 300;
  font-size: 1.5em;
  margin-left: 20px;
}

.order-header {
  color: #ffffff;
  font-family: "Montserrat", sans-serif;
  font-weight: 300;
  font-size: 15px;
  margin-top: 10px;
  margin-left: 20px;
  cursor: pointer;
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

  .coffee-container:hover {
    background-color: #815341;
    cursor: pointer;
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

.modal-content {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 10px;
}

.coffee-price-modal {
  font-size: 30px;
  font-weight: 500;
  margin-top: 20px;
}

.input-group-modal {
  display: flex;
  align-items: center;
}

.input-field {
  margin-left: 8px; 
  margin-right: 8px;
}

/* Hide spinner controls in modern browsers */
input[type="number"]::-webkit-inner-spin-button,
input[type="number"]::-webkit-outer-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

</style>
