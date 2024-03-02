<template>
    <h1>Here</h1>
    <div>
        {{mortgageApplicant}}
    </div>
</template>

<script setup lang="ts">
    import { ref } from 'vue'

    interface Applicant {
        name: string,
        principleAmount: number;
        annualRate: number;
        loanYears: 0;
    }

    const mortgageApplicant: Applicant = ref({name: '', principleAmount: 0, annualRate: 0, loanYears: 0})
        
    
</script>