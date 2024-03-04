<template>
  <div id="mortgage-display" class="mortgage-display">
    <h3>Mortgage Amortization Calculator</h3>
    <span v-if="error" class="error-message">{{ error.toUpperCase() }}</span>
    <div class="applicant-field">
      <div id="labels" class="applicant-column">
        <label class="spacing">Principle Amount</label>
        <label class="spacing">Annual Rate</label>
        <label class="spacing">Loan Term</label>
        <label class="spacing">Start Date</label>
      </div>
      <div id="fields" class="applicant-column">
        <input v-model="principleAmount">
        <input v-model="annualRate">
        <input v-model="loanYears">
        <input v-model="startDate" type="date">
      </div>
    </div>
    <button class="submit-button" @click="generateRates()">Generate loan rates</button>
    <div v-if="recordAvailable" id="navigation">
      <button v-if="currentRecord.index != 0" @click="back()">View Last Record</button>
      <button v-if="currentRecord.index != userMortgageRecord.Schedules.length" @click="forward()">View Next Record
      </button>
    </div>
    <MortgageDisplay v-if="recordAvailable" :mortgage-record="currentRecord.schedule.Payments"></MortgageDisplay>
  </div>
</template>

<script lang="ts" setup>
import {onMounted, ref} from 'vue'
import type {Applicant, MortgagePaymentByMonth, Schedule} from "../../types/utilTypes";
import MortgageDisplay from "@/components/MortgageDisplay.vue";


const name = ref('')
const principleAmount = ref(0)
const annualRate = ref(0)
const loanYears = ref(0)
const error = ref<string>();
const startDate = ref<Date>();

const recordAvailable = ref<boolean>()

let userMortgageRecord: MortgagePaymentByMonth
let currentRecord: ref<{ index: number, schedule: Schedule }>;

onMounted(async () => {
  const userId: string | null = localStorage.userId

  if (userId) {
    const response = await fetch(`http://localhost:5137/applicant/${userId}`)
    userMortgageRecord = await response.json();
    if (userMortgageRecord?.Schedules) {
      const index = userMortgageRecord.Schedules.length - 1
      currentRecord.value = {index: index, schedule: userMortgageRecord.Schedules[index]}

      recordAvailable.value = true
    }
  }
})

function back() {
  debugger
  currentRecord.index = currentRecord.index - 1
  currentRecord.schedule = userMortgageRecord.Schedules[currentRecord.index]
}

function forward() {
  debugger
  currentRecord.index = currentRecord.index + 1
  currentRecord.schedule = userMortgageRecord.Schedules[currentRecord.index]
}

async function generateRates() {
  recordAvailable.value = false

  if (!principleAmount.value || !annualRate.value || !loanYears.value || !startDate.value) {
    error.value = "Please enter your name and amounts greater than zero"
    return
  }

  //reset errors if user had any
  error.value = ""


  const applicant: Applicant = {
    principleAmount: principleAmount.value,
    annualRate: annualRate.value / 100,
    loanMonths: loanYears.value * 12,
    startDate: startDate.value
  }

  const userId = localStorage.userId
  if (userId) {
    await updateUser(userId, applicant)
  } else {
    await createNewUser(applicant)
  }

}

async function createNewUser(applicant: Applicant) {
  try {

    const response = await fetch('http://localhost:5137/applicant', {
      method: "POST",
      body: JSON.stringify(applicant),
      headers: {
        "Content-Type": "application/json"
      }
    })

    userMortgageRecord = await response.json();
    if (userMortgageRecord?.Schedules) {
      const index = userMortgageRecord.Schedules.length - 1
      currentRecord = {index: index, schedule: userMortgageRecord.Schedules[index]}

      recordAvailable.value = true

      // save data for multiple sessions
      localStorage.setItem('userId', userMortgageRecord.Id)
    }
  } catch (e) {
    console.log("Failed to post applicant data", e)
    error.value = "There was an issue generating rates, please try again"
  }
}

async function updateUser(id: string, applicant: Applicant) {
  try {
    const response = await fetch(`http://localhost:5137/applicant/update/${id}`, {
      method: "PUT",
      body: JSON.stringify(applicant),
      headers: {
        "Content-Type": "application/json"
      }
    })

    userMortgageRecord = await response.json();
    if (userMortgageRecord?.Schedules) {
      const index = userMortgageRecord.Schedules.length - 1
      currentRecord = {index: index, schedule: userMortgageRecord.Schedules[index]}

      recordAvailable.value = true
    }
  } catch (e) {
    console.log("Failed to post applicant data", e)
    error.value = "There was an issue generating rates, please try again"
  }
}


</script>

<style scoped>

h3 {
  font-family: "Cascadia Code";
}

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
  margin-bottom: 24px;
}

.applicant-column {
  padding: 8px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  justify-content: center;
}

.submit-button {
  padding: 12px;
  max-width: 300px;
  margin: 0 auto;
  margin-bottom: 24px;
}

.error-message {
  background-color: orangered;
}

</style>