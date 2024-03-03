<template>
  <div id="mortgage-display">
    <table>
      <thead class="header">
        <th>Payment Date</th>
        <th>Remaining Balance</th>
        <th>Principle Paid</th>
        <th>Interest Paid</th>
        <th>Payment</th>
        <th>Total Interest</th>
        <th>Total Paid</th>
      </thead>
      <tbody>
        <tr id="table-contents" class="table-body" v-for="record in mortgageRecord.MonthlyMortgagePayment" :key="record.PaymentDate">
          <td>{{record.PaymentDate }}</td>
          <td>{{record.RemainingBalance }}</td>
          <td>{{record.PrinciplePaid }}</td>
          <td>{{record.InterestPaid}}</td>
          <td>{{record.Payment}}</td>
          <td>{{record.TotalInterest}}</td>
          <td>{{record.TotalPayment}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts" setup>
import type { MortgagePaymentByMonth } from "@/../types/applicantTypes";

const props = defineProps<{ mortgageRecord: MortgagePaymentByMonth }>()

console.log('payment', props.mortgageRecord)
</script>

<style scoped>
  .header {
    background: gainsboro;
    th {
      width: 25%;
    }
  }
  .table-body {
    background: grey;
  }
</style>
