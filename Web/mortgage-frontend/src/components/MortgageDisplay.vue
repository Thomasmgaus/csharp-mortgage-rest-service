<template>
  <div id="mortgage-display">
    <table v-if="test">
      <div id="table-header">

        <th>Payment Date</th>
        <th>Remaining Balance</th>
        <th>Principle Paid</th>
        <th>Interest Paid</th>
        <th>Payment</th>
        <th>Total Interest</th>
        <th>Total Paid</th>

      </div>
      <div id="table-contents"  v-for="record in test.monthlyMortgagePayment" :key="record.paymentDate">
        <tr>
          <td>{{record.paymentDate }}</td>
          <td>{{record.interestPaid }}</td>
          <td>{{record.principlePaid }}</td>
          <td>{{record.interestPaid}}</td>
          <td>{{record.monthlyPayment}}</td>
          <td>{{record.totalInterest}}</td>
          <td>{{record.totalPayment}}</td>
        </tr>
      </div>
    </table>
  </div>
</template>

<script lang="ts" setup>
import type { MortgagePaymentByMonth } from "@/../types/applicantTypes";
import {ref, type Ref} from "vue";

const props = defineProps<{ mortgageRecord: Ref<MortgagePaymentByMonth> }>()

const test = ref(props.mortgageRecord)

console.log("lol what is this", test.value)
console.log("lol what is this2", props.mortgageRecord.value)
</script>
