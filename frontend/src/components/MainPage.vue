<template>
  <div :class="{ 'blur-background': isAddModalVisible || isOrderModalVisible }" class="main-page">
    <div class="main-page-header">
      <h1 class="header-title">Vale's Coffee Nest</h1>
      <div class="header-info">
        <div class="total-price">
          Total: ₡{{ priceTotal }}
        </div>
        <button @click="showOrderModal()" class="order-header button-custom">Pagar mi orden</button>
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

  <ModalComponent
    :isVisible="isOrderModalVisible"
    title="Orden"
    @close="isOrderModalVisible = false"
  >
    <template #body>
      <div class="modal-content">
        <div class="coffee-content" v-for="coffee in selectedCoffees" :key="coffee.coffeeType">
          <div class="coffee-image-container">
            <img :src="getImage(coffee.image)" alt="Coffee Image" class="coffee-image-modal">
          </div>
          <div class="coffee-details">
            <p>{{ coffee.coffeeType }} - ₡{{ coffee.price }} x {{ coffee.quantity }}</p>
          </div>
        </div>
        <p>Total: ₡{{ priceTotal }}</p>
      </div>
      <div class="payment-modal">
        Sólo aceptamos efectivo (billetes de ₡1000 y monedas de ₡500, ₡100, ₡50 y ₡25).
        <p>Voy a pagar con:</p>
        <p>Billetes de 1000<input type="number" v-model="bills" placeholder="0" class="input-field" min="0"/></p>
        Monedas:
        <p>500<input type="number" v-model="coin500" placeholder="0" class="input-field" min="0"/></p>
        <p>100<input type="number" v-model="coin100" placeholder="0" class="input-field" min="0"/></p>
        <p>50<input type="number" v-model="coin50" placeholder="0" class="input-field" min="0"/></p>
        <p>25<input type="number" v-model="coin25" placeholder="0" class="input-field" min="0"/></p>
        <p>Efectivo total: ₡{{ totalCash }}</p>
      </div>
    </template>
    <template v-slot:footer>
      <button @click="checkPayment" type="button" class="custom-button custom-font white-font">Pagar</button>
      <button @click="isOrderModalVisible = false" type="button" class="custom-button custom-font white-font">Cerrar</button>
    </template>
  </ModalComponent>

  <ModalComponent
    :isVisible="isMessageVisible"
    :title="modalTitle"
    @close="isMessageVisible = false"
  >
    <template #body>
      <div class="modal-content">
        <p v-html="modalMessage"></p>
      </div>
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
      modalTitle: '',
      modalMessage: '',
      isMessageVisible: false,
      coffeeList: [],
      selectedCoffees: [],
      isAddModalVisible: false,
      isOrderModalVisible: false,
      selectedCoffee: null,
      quantity: 1,
      priceTotal: 0,
      bills: 0,
      coin500: 0,
      coin100: 0,
      coin50: 0,
      coin25: 0,
      cash: [],
      cashEntered: [],
      changeMessage: '',
      isServiceAvailable: true,
    }
  },
  computed: {
    totalCash() {
      return this.bills * 1000 + this.coin500 * 500 + this.coin100 * 100 + this.coin50 * 50 + this.coin25 * 25;
    }
  },
  methods: {
    async getCoffeeList() {
      try {
        const response = await axios.get(`${BACKEND_URL}/Coffee`);
        this.coffeeList = response.data;
      } catch (error) {
        if (error.response) {
          if (error.response.status === 400) {
            this.displayError(error.response.data);
          } else {
            this.displayError("An unexpected error occurred.");
          }
        } else if (error.request) {
          this.displayError("Unable to connect to the server. Please try again later.");
        } else {
          this.displayError(error.message);
        }
      }
    },
    displayError(message) {
      this.modalTitle = 'Error';
      this.modalMessage = message;
      this.isMessageVisible = true;
    },
    async getCash(){
      try {
        const response = await axios.get(`${BACKEND_URL}/Cash`);
        this.cash = response.data;
        this.cash = this.cash.reduce((acc, item) => {
          acc[`coin${item.denomination}`] = item.quantity;
          return acc;
        }, {});
      } catch (error) {
        if (error.response) {
          if (error.response.status === 400) {
            this.displayError(error.response.data);
          } else {
            this.displayError("An unexpected error occurred.");
          }
        } else if (error.request) {
          this.displayError("Unable to connect to the server. Please try again later.");
        } else {
          this.displayError(error.message);
        }
      }
    },
    getImage(imagePath) {
      const url = `https://localhost:7263${imagePath}`;
      return url;
    },
    showModalCoffee() {
      this.isAddModalVisible = true;
    },
    selectCoffee(coffee) {
      this.selectedCoffee = coffee;
      this.quantity = 1;
      this.showModalCoffee();
    },
    increaseQuantity() {
      if (this.quantity < this.selectedCoffee.quantity) {
        this.quantity++;
      } else {
        this.displayError("No hay más café disponible de este tipo.");
      }
    },
    decreaseQuantity() {
      if (this.quantity > 0) {
        this.quantity--;
      }
    },
    addCoffee() {
      if (this.selectedCoffee.quantity >= this.quantity) {
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
            image: this.selectedCoffee.image,
          });
        }
        this.priceTotal += this.selectedCoffee.price * this.quantity;
        this.isAddModalVisible = false;
        this.modalTitle = 'Operación exitosa';
        this.modalMessage = "¡Café añadido a la orden!";
        this.isMessageVisible = true;
      } else {
        this.displayError("No hay suficientes existencias disponibles.");
      }
    },
    showOrderModal() {
      this.isOrderModalVisible = true;
      this.getCash();
    },
    async checkPayment() {
      this.isOrderModalVisible = false;
      if (!this.isServiceAvailable) {
        this.displayError("Servicio fuera de servicio. No hay suficientes monedas para dar vuelto.");
        return;
      }
      const change = this.totalCash - this.priceTotal;
      if (change < 0) {
        this.displayError("Monto insuficiente para completar la compra.");
        return;
      }
      
      const { success, details: breakdown } = this.calculateChange(change);
      if (!success) {
        this.displayError("Fallo al realizar la compra. No hay suficientes monedas para el vuelto.");
        return;
      }

      // Inline display success logic
      this.modalTitle = 'Operación exitosa';
      this.modalMessage = `Su vuelto es de ${change} colones.<br>Desglose:<br>` +
        `${breakdown.coin500} moneda(s) de 500<br>` +
        `${breakdown.coin100} moneda(s) de 100<br>` +
        `${breakdown.coin50} moneda(s) de 50<br>` +
        `${breakdown.coin25} moneda(s) de 25`;
      this.isMessageVisible = true;
      this.resetCoins();

      this.resetOrder();
          
    },
    calculateChange(change) {
      const breakdown = {
        coin500: 0,
        coin100: 0,
        coin50: 0,
        coin25: 0,
      };
      const denominations = [
        { name: 'coin500', value: 500 },
        { name: 'coin100', value: 100 },
        { name: 'coin50', value: 50 },
        { name: 'coin25', value: 25 },
      ];

      for (const { name, value } of denominations) {
        while (change >= value && this.cash[name] > 0) {
          change -= value;
          this.cash[name]--;
          breakdown[name]++;
        }
      }

    return {
      success: change === 0,
      details: breakdown,
    };
  },

  resetOrder() {
    this.selectedCoffees = [];
    this.priceTotal = 0;
  },
  resetCoins() {
    this.bills = 0;
    this.coin500 = 0;
    this.coin100 = 0;
    this.coin50 = 0;
    this.coin25 = 0;
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

.button-custom {
  background-color: #A18363;
  color: #ffffff;
  border: none;
  border-radius: 5px;
  padding: 5px;
  font-size: 16px;
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

.coffee-content {
  display: flex;
  justify-content: space-between;
}

.coffee-image-container {
  flex-shrink: 0;
}

.coffee-image-modal {
  width: 80px;
  height: 80px;
  align-self: flex-start;
}

.coffee-details {
  margin-left: 15px;
  flex: 1;
  text-align: left;
}

.input-group-modal {
  display: flex;
  align-items: center;
}

.input-field {
  margin-left: 8px; 
  margin-right: 8px;
  flex: 1;
}

</style>
