<template>
  <div id="mortgage-display" class="mortgage-display">
    <h3>Mortgage Repayment calculator</h3>
    <span v-if="error">{{ error.toUpperCase() }}</span>
    <div class="applicant-field">
      <div id="labels" class="applicant-column">
        <label class="spacing">Name</label>
        <label class="spacing">Principle Amount</label>
        <label class="spacing">Annual Rate</label>
        <label class="spacing">Loan Term</label>
        <label class="spacing">Start Date</label>
      </div>
      <div id="fields" class="applicant-column">
        <input v-model="name">
        <input v-model="principleAmount">
        <input v-model="annualRate">
        <input v-model="loanYears">
        <input v-model="startDate" type="date">
      </div>
    </div>
    <button class="submit-button" @click="generateRates()">Generate loan rates</button>
    <MortgageDisplay v-if="recordAvailable" :mortgage-record="userMortgageRecord"></MortgageDisplay>
  </div>
</template>

<script lang="ts" setup>
import {reactive, ref} from 'vue'
import type {Applicant, MortgagePaymentByMonth} from "@/../types/applicantTypes";
import MortgageDisplay from "@/components/MortgageDisplay.vue";



const name = ref('')
const principleAmount = ref(0)
const annualRate = ref(0)
const loanYears = ref(0)
const error = ref<string>();
const startDate = ref<Date>();

let recordAvailable = ref<boolean>()
let userMortgageRecord: MortgagePaymentByMonth = {
  id: '',
  monthlyMortgagePayment: []
}

async function generateRates() {
  if (!name.value || !principleAmount.value || !annualRate.value || !loanYears.value || !startDate.value) {
    error.value = "Please enter your name and amounts greater than zero"
    return
  }

  //reset errors if user had any
  error.value = ""

  const applicant: Applicant = {
    name: name.value,
    principleAmount: principleAmount.value,
    annualRate: annualRate.value / 100,
    loanMonths: loanYears.value * 12,
    startDate: startDate.value
  }

  try {

    const response = await fetch('http://localhost:5137/applicant', {
      method: "POST",
      body: JSON.stringify(applicant),
      headers: {
        "Content-Type": "application/json"
      }
    })

    userMortgageRecord = await response.json();
    if(userMortgageRecord.MonthlyMortgagePayment.length > 0) {
      recordAvailable.value = true
    }
  } catch (e) {
    console.log("Failed to post applicant data", e)
    error.value = "There was an issue generating rates, please try again"
  }

}


</script>

<style scoped>

.mortgage-display {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.applicant-field {
  display: flex;
  border-style: groove;
  border-bottom-color: black;
  background: darkgrey;
  max-width: 500px;
  max-height: 300px;
  margin: 0 auto;
}

.applicant-column {
  padding: 8px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  justify-content: center;
}

.submit-button {
  margin: 12px;
  padding: 12px;
  max-width: 300px;
  margin: 0 auto;
}


</style>