<template>
  <div id="mortgage-display" class="mortgage-display">
    <h3>Mortgage Repayment calculator</h3>
    <span v-if="error">{{ error.toUpperCase() }}</span>
    <div class="applicant-field">
      <div id="labels" class="applicant-column">
        <label class="spacing">Name</label>
        <label class="spacing">Principle Amount</label>
        <label class="spacing">Annual Rate</label>
        <label class="spacing">Loan Years</label>
      </div>
      <div id="fields" class="applicant-column">
        <input v-model="mortgageApplicant.name">
        <input v-model="mortgageApplicant.principleAmount">
        <input v-model="mortgageApplicant.annualRate">
        <input v-model="mortgageApplicant.loanMonths">
      </div>
    </div>
    <button class="submit-button" @click="generateRates()">Generate loan rates</button>
  </div>
</template>

<script lang="ts" setup>
import {ref} from 'vue'

type Applicant = {
  name: string,
  principleAmount: number,
  annualRate: number,
  loanYears: number,
}

const mortgageApplicant = ref<Applicant>({name: 'Tom', principleAmount: 160000, annualRate: 0.035, loanMonths: 220})
const error = ref<string>();

async function generateRates() {
  const applicant: Applicant = mortgageApplicant.value;
  if (!applicant.name || !applicant.principleAmount || !applicant.annualRate || !applicant.loanMonths) {
    error.value = "Please enter your name and amounts greater than zero"
    return
  }
  //reset errors if user had any
  error.value = ""

  console.log('Applicant', JSON.stringify(applicant))

  try {
    const tst = await fetch('http://localhost:5137/applicant', {
      method: "POST",
      body: JSON.stringify(applicant),
      headers: {
        "Content-Type": "application/json"
      }
    })
    const result = await tst.json();
    console.log('test ', result)
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