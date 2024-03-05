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
      <tr v-for="(record, index) in mortgageRecord" id="table-contents" :key="record.PaymentDate"
          :class="index % 2 === 0 ? 'table-body-one' : 'table-body-two' ">
        <td>{{ record.PaymentDate }}</td>
        <td>{{ record.RemainingBalance }}</td>
        <td>{{ record.PrinciplePaid }}</td>
        <td>{{ record.InterestPaid }}</td>
        <td>{{ record.Payment }}</td>
        <td>{{ record.TotalInterest }}</td>
        <td>{{ record.TotalPayment }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts" setup>
import type {MonthlyPayment} from "../../types/utilTypes";

const props = defineProps<{ mortgageRecord: MonthlyPayment[] }>()

</script>

<style scoped>
.header {
  background: gainsboro;

  th {
    width: 25%;
  }
}

.table-body-one {
  background: grey;
}

.table-body-two {
  background: lightgrey;
}
</style>
