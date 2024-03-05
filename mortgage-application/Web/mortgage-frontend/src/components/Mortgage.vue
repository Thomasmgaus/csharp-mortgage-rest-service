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
    <div v-if="currentRecord" id="navigation">
      <button :disabled="!showBackButton" @click="back()">View Last Record</button>
      <button :disabled="!showForwardButton" @click="forward()">View Next Record</button>
    </div>
    <MortgageDisplay v-if="currentRecord"
                     :mortgage-record="currentRecord.Payments"></MortgageDisplay>
  </div>
</template>

<script lang="ts" setup>
import {computed, onMounted, ref, watch} from 'vue'
import type {Applicant, MortgagePaymentByMonth, Schedule} from "../../types/utilTypes";
import MortgageDisplay from "@/components/MortgageDisplay.vue";

// state variables
const principleAmount = ref(0)
const annualRate = ref(0)
const loanYears = ref(0)
const error = ref<string>();
const startDate = ref<Date>();

let recordIndex = ref<number>(0);
let userMortgageRecord = ref<MortgagePaymentByMonth>()

// computed variables
const currentRecord = computed(() => {
  if(!userMortgageRecord.value) {
    return undefined
  } else {
    return userMortgageRecord.value?.Schedules[recordIndex.value]
  }
})

const showBackButton = computed(() => recordIndex.value > 0)

const showForwardButton = computed(() => {
  return userMortgageRecord.value &&
      userMortgageRecord.value.Schedules?.length > 1
      && recordIndex.value != userMortgageRecord.value?.Schedules.length - 1
})

// watchers
watch(currentRecord, () => {
  if(currentRecord.value) {
    setProperties(currentRecord.value)
  }
})

onMounted(async () => {
  const userId: string | null = localStorage.userId

  if (userId) {
    const response = await fetch(`http://localhost:5137/applicant/${userId}`)
    userMortgageRecord.value = await response.json();
    if (userMortgageRecord?.value?.Schedules) {
      recordIndex.value = userMortgageRecord.value.Schedules.length - 1
    }
  }
})

function back() {
  if (currentRecord?.value) {
    recordIndex.value -= 1
  }
}

function forward() {
  if (currentRecord?.value) {
    recordIndex.value += 1
  }
}

function setProperties(schedule: Schedule) {
  if (!schedule) return

  principleAmount.value = schedule.PrincipleAmount
  annualRate.value = schedule.AnnualRate
  loanYears.value = schedule.LoanYears
}

async function generateRates() {
  if (!principleAmount.value || !annualRate.value || !loanYears.value || !startDate.value) {
    error.value = "Please enter a valid date and amounts greater than zero"
    return
  }

  //reset errors if user had any
  error.value = ""


  const applicant: Applicant = {
    principleAmount: principleAmount.value,
    annualRate: annualRate.value,
    loanYears: loanYears.value,
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

    userMortgageRecord.value = await response.json();
    if (userMortgageRecord.value?.Schedules) {
      recordIndex.value = userMortgageRecord.value.Schedules.length - 1
      // save data for multiple sessions
      localStorage.setItem('userId', userMortgageRecord.value.Id)
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

    userMortgageRecord.value = await response.json();
    if (userMortgageRecord.value?.Schedules) {
      recordIndex.value = userMortgageRecord.value.Schedules.length - 1
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