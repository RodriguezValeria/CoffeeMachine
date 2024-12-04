<template>
  <div class="main-page">
    <div class="main-page-header">
      <h1 class="header-title">Vale's Coffee Nest</h1>
    </div>
    <div class="coffee-main-section">
      Seleccione su café de preferencia:
    </div>
  </div>
</template>

<script>
export default {
  name: 'MainPage',
  props: {
    msg: String
  }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&family=Mooli&display=swap');

.main-page-header {
  background-color: #310603;
  padding: 50px;
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
</style>
