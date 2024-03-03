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
    <div v-if="userMortgageRecord"> {{"Here"}} </div>
  </div>
</template>

<script lang="ts" setup>
import {ref} from 'vue'
import {ModuleNode} from "vite";

type Applicant = {
  name: string,
  principleAmount: number,
  annualRate: number,
  loanMonths: number,
  startDate: Date,
}

type MonthlyPayment = {
  paymentDate: string,
  interestPaid: number,
  principlePaid: number,
  MonthlyPayment: number,
  TotalInterest: number,
  TotalPayment: number,
}

type MortgagePaymentByMonth = {
  id: string,
  monthlyMortgagePayment: MonthlyPayment[]
}

const name = ref('')
const principleAmount = ref(0)
const annualRate = ref(0)
const loanYears = ref(0)
const error = ref<string>();
const startDate = ref<Date>();

let userMortgageRecord = ref<MortgagePaymentByMonth>();

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

    const tst = await fetch('http://localhost:5137/applicant', {
      method: "POST",
      body: JSON.stringify(applicant),
      headers: {
        "Content-Type": "application/json"
      }
    })

    userMortgageRecord.value = await tst.json();

    console.log(result)
  } catch (e) {
    console.log("Failed to post applicant data")
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